using AutoMapper;

using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.DTOs.Account.ConfirmAccount;
using RealState.Application.DTOs.Account.ForgotPassword;
using RealState.Application.DTOs.Account.ResetPassword;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Account;
using RealState.Application.ViewModel.User;

namespace RealState.Application.Services
{
    public class AccountServices(IIdentityServices identityServices, IMapper mapper, IRoleRepository roleRepository) : IAccountServices
    {
        private readonly IIdentityServices _identityServices = identityServices;
        private readonly IMapper _mapper = mapper;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<Result<Unit>> ConfirmAccountAsync(ConfirmAccountViewModel confirmAccount)
        {
            var request = _mapper.Map<ConfirmAccountRequestDTO>(confirmAccount);
            var result = await _identityServices.ConfirmAccountAsync(request);
            if (!result.Success)
                return ErrorType.Conflict.Because(result.Error);

            return Unit.T;
        }

        public async Task<Result<Unit>> ForgotPasswordAsync(ForgotPasswordViewModel forgotPassword)
        {
            var request = _mapper.Map<ForgotPasswordRequestDTO>(forgotPassword);
            var result = await _identityServices.ForgotPasswordAsync(request);
            if (!result.Success)
                return ErrorType.Conflict.Because(result.Error);

            return Unit.T;
        }

        public async Task<Result<ApplicationUserDTO>> RegisterAsync(UserSaveViewModel saveUser)
        {
            var roles  = _roleRepository.GetRolesById(saveUser.RolesId);
            var request = _mapper.Map<SaveApplicationUserDTO>(saveUser);
            request.Roles = roles.ToList();
            var result = await _identityServices.RegisterAsync(request);
            if (!result.Success)
                return ErrorType.Conflict.Because(result.Error);

            return result.NewUser!;
        }

        public async Task<Result<Unit>> ResetPasswordAsync(ResetPasswordViewModel resetPassword)
        {
            var request = _mapper.Map<ResetPasswordRequestDTO>(resetPassword);
            var result = await _identityServices.ResetPasswordAsync(request);
            if (!result.Success)
                return ErrorType.Conflict.Because(result.Error);

            return Unit.T;
        }

        public async Task<Result<ApplicationUserDTO>> SignInAsync(LoginViewModel login)
        {
            var request = _mapper.Map<AuthenticationRequestDTO>(login);
            var result = await _identityServices.AuthenticationAsync(request);
            if (!result.Success)
                return ErrorType.Conflict.Because(result.Error!);
            if (result.CurrentUser.Roles.Any(static x => x.Name == nameof(RoleTypes.Developer)))
            {
                await SignOutAsync();
                return "A developer cannot log in to a web app";
            }

            return result.CurrentUser;
        }

        public async Task SignOutAsync()
        {
            await _identityServices.SignOutAsync();
        }
    }
}
