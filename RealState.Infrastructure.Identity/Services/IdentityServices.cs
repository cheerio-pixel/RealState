using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.DTOs.Account.ConfirmAccount;
using RealState.Application.DTOs.Account.ForgotPassword;
using RealState.Application.DTOs.Account.RegisterResponse;
using RealState.Application.DTOs.Account.ResetPassword;
using RealState.Application.DTOs.EmailServices;
using RealState.Application.DTOs.Role;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Domain.Settings;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Services
{
    public class IdentityServices
       (
       UserManager<ApplicationUser> userManager,
       RoleManager<ApplicationRole> roleManager,
       SignInManager<ApplicationUser> signInManager,
       IMapper mapper,
       IUriServices uriServices,
       IEmailServices emailServices,
       IOptions<JWTSettings> jwtSettings,
       IRoleRepository roleRepository
       )
       : IIdentityServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUriServices _uriServices = uriServices;
        private readonly IEmailServices _emailServices = emailServices;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly JWTSettings _jwtSettings = jwtSettings.Value;

        public async Task<AuthenticationResponseDTO> AuthenticationAsync(AuthenticationRequestDTO request)
        {
            #region Validations
            ApplicationUser? user = IsEmailAccount(request.Account) switch
            {
                true => await _userManager.FindByEmailAsync(request.Account),
                false => await _userManager.FindByNameAsync(request.Account)
            };

            if (user is null)
                return new()
                {
                    Success = false,
                    Error = "Sorry, we couldn't find your account."
                };

            if (!user.EmailConfirmed)
            {
                string msg;
                if (await _userManager.IsInRoleAsync(user, nameof(RoleTypes.Client)))
                {
                    msg = "Your account hasn't been confirmed. Check your email.";
                }
                else
                {
                    msg = "Your account is currently unactive, get in contact with an administrator.";
                }
                return new()
                {
                    Success = false,
                    Error = msg
                };
            }
            #endregion

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = "The password is incorrect."
                };

            var userDTO = _mapper.Map<ApplicationUserDTO>(user);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            userDTO.Roles = roles.Select(x => new ApplicationRoleDTO(x)).ToList();

            var token = await GenerateJWTokenAsync(user);

            return new()
            {
                CurrentUser = userDTO,
                Success = true,
                JWToken = token,
                RefreshToken = GenerateRefreshToken().Token
            };
        }

        public async Task<ConfirmAccountResponseDTO> ConfirmAccountAsync(ConfirmAccountRequestDTO request)
        {
            var userById = await _userManager.FindByIdAsync(request.UserID);
            if (userById is null)
                return new()
                {
                    Error = $"There isn't any user with this id: {request.UserID}.",
                    Success = false
                };

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ConfirmEmailAsync(userById, token);
            if (!result.Succeeded)
                return new()
                {
                    Error = result.Errors.First().Description,
                    Success = false
                };
            return new() { Success = true };
        }

        public async Task<ForgotPasswordResponseDTO> ForgotPasswordAsync(ForgotPasswordRequestDTO request)
        {
            #region Validations
            ApplicationUser? user;
            switch (IsEmailAccount(request.Account))
            {
                case true:
                    user = await _userManager.FindByEmailAsync(request.Account);
                    break;
                case false:
                    user = await _userManager.FindByNameAsync(request.Account);
                    break;
            }

            if (user is null)
                return new()
                {
                    Success = false,
                    Error = "Sorry, we couldn't find your account."
                };

            if (!user.EmailConfirmed)
                return new()
                {
                    Success = false,
                    Error = "You need to confirm your account to log in."
                };
            #endregion

            var resultOfSendEmail = await SendResetPasswordEmail(user);
            if (!resultOfSendEmail)
                return new()
                {
                    Success = false,
                    Error = "There was a problem while sending the email"
                };
            return new() { Success = true };
        }

        public async Task<RegisterResponseDTO> RegisterAsync(SaveApplicationUserDTO saveUser)
        {
            #region Validations
            var userByName = await _userManager.FindByNameAsync(saveUser.UserName);
            if (userByName is not null)
                return new()
                {
                    Success = false,
                    Error = $"The UserName: {saveUser.UserName} is already taken"
                };

            var userByEmail = await _userManager.FindByEmailAsync(saveUser.Email);
            if (userByEmail is not null)
                return new()
                {
                    Success = false,
                    Error = $"The email: {saveUser.Email} is already taken"
                };

            var userByIdentifierCard = _userManager.Users.Any(x => x.IdentifierCard == saveUser.IdentifierCard);
            if (userByIdentifierCard)
                return new()
                {
                    Success = false,
                    Error = $"The IdentifierCard: {saveUser.IdentifierCard} is already taken"
                };
            var user = _mapper.Map<ApplicationUser>(saveUser);

            //verify if user is a manager or not
            var managerRolesName = _roleRepository.GetManagementRoles().Select(x => x.Name).ToList();
            var isUserManager = saveUser.Roles.Any(x => managerRolesName.Contains(x.Name));
            if (isUserManager)
                user.EmailConfirmed = true;
            #endregion

            #region Create user
            user.Roles.Clear();
            var result = await _userManager.CreateAsync(user, saveUser.Password);
            if (!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = result.Errors.First().Description
                };

            result = await _userManager.AddToRolesAsync(user, saveUser.Roles.Select(x => x.Name));
            if (!result.Succeeded)
                return new()
                {
                    Success = false,
                    Error = result.Errors.First().Description
                };
            #endregion

            var userDto = _mapper.Map<ApplicationUserDTO>(user);

            #region verify send email to confirm account
            if (!saveUser.Roles.Select(x => x.Name).Contains(RoleTypes.Client.ToString()))
                return new()
                {
                    NewUser = userDto,
                    Success = true,
                };

            var resultOfSendEmail = await SendConfirmAccountEmal(user);
            if (!resultOfSendEmail)
                return new()
                {
                    Success = false,
                    Error = "There was a problem while sending the email"
                };
            #endregion

            return new()
            {
                NewUser = userDto,
                Success = true,
            };
        }

        public async Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            var userById = await _userManager.FindByIdAsync(request.UserID);
            if (userById is null)
                return new()
                {
                    Error = $"There isn't any user with this id: {request.UserID}.",
                    Success = false
                };

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ResetPasswordAsync(userById, token, request.Password);
            if (!result.Succeeded)
                return new()
                {
                    Error = result.Errors.First().Description,
                    Success = false
                };
            return new() { Success = true };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region Private
        private async Task<bool> SendResetPasswordEmail(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var url = _uriServices.GetUrl(code, user.Id, "ResetPassword");
            var email = new EmailRequestDTO()
            {
                To = user.Email!,
                Subject = "Reset your account",
                Body = $"Please reset your password here: {url}"
            };

            var result = await _emailServices.SendAsync(email);
            return result;
        }

        private async Task<bool> SendConfirmAccountEmal(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var url = _uriServices.GetUrl(code, user.Id, "ConfirmAccount");

            var email = new EmailRequestDTO()
            {
                To = user.Email!,
                Subject = "Confirm Your Account",
                Body = $"Please, confirm your account here {url}"
            };
            var result = await _emailServices.SendAsync(email);
            return result;
        }

        private bool IsEmailAccount(string account)
        {
            string patron = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            var regex = new Regex(patron);
            return regex.IsMatch(account);
        }

        private async Task<string> GenerateJWTokenAsync(ApplicationUser user)
        {
            #region Header
            var symmetricSecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signinCredentials = new SigningCredentials(symmetricSecretKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signinCredentials);
            #endregion

            #region Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));

            var tokenClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("userId", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            #endregion

            #region Payload
            var payload = new JwtPayload
                (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: tokenClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
                );
            #endregion

            #region Token
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
            #endregion
        }

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken()
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        public string RandomTokenString()
        {
            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
            var ramdonBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdonBytes);

            return BitConverter.ToString(ramdonBytes).Replace("_", "");
        }
        #endregion
    }
}
