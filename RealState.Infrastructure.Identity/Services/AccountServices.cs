using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.DTOs.Account.ConfirmAccount;
using RealState.Application.DTOs.Account.ForgotPassword;
using RealState.Application.DTOs.Account.RegisterResponse;
using RealState.Application.DTOs.Account.ResetPassword;
using RealState.Application.DTOs.EmailServices;
using RealState.Application.DTOs.Role;
using RealState.Application.DTOs.User;
using RealState.Application.Interfaces.Services;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Services
{
    public class AccountServices
        (
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IUriServices uriServices,
        IEmailServices emailServices
        ) 
        : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUriServices _uriServices = uriServices;
        private readonly IEmailServices _emailServices = emailServices;

        public async Task<AuthenticationResponseDTO> AuthenticationAsync(AuthenticationRequestDTO request)
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

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure :  false);
            if (!result.Succeeded)
                return new() 
                { 
                    Success = false,
                    Error = "The password is incorrect."
                };

            var userDTO = _mapper.Map<ApplicationUserDTO>(user);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            userDTO.Roles = roles.ToList().ConvertAll(x => new ApplicationRoleDTO(x));

            return new() 
            { 
                CurrentUser = userDTO,
                Success = true
            };
        }

        public async Task<ConfirmAccountResponseDTO> ConfirmAccountAsync(ConfirmAccountRequestDTO request)
        {
            var userById = await _userManager.FindByIdAsync(request.UserID);
            if(userById is null)
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

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var url = _uriServices.GetUrl(code, user.Id, "ResetPassword");
            var email = new EmailRequestDTO() 
            { 
                To = user.Email,
                Subject = "Reset your account",
                Body = $"Please reset your password here: {url}"
            };

            await _emailServices.SendAsync(email);
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
            #endregion

            #region Create user
            var user = _mapper.Map<ApplicationUser>(saveUser);

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

            #region Send email to confirm account
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var url = _uriServices.GetUrl(code, user.Id, "ConfirmAccount");

            var email = new EmailRequestDTO()
            {
                To = user.Email,
                Subject = "Confirm Your Account",
                Body = $"Please, confirm your account here {url}"
            };
            await _emailServices.SendAsync(email);
            #endregion

            var userDto = _mapper.Map<ApplicationUserDTO>(user);
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
        private bool IsEmailAccount(string account)
        {
            string patron = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            var regex = new Regex(patron);
            return regex.IsMatch(account);
        }
        #endregion
    }
}
