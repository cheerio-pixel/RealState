using RealState.Application.DTOs.User;
using RealState.Application.Extras.ResultObject;
using RealState.Application.ViewModel.Account;
using RealState.Application.ViewModel.User;

namespace RealState.Application.Interfaces.Services
{
    public interface IAccountServices
    {
        public Task<Result<ApplicationUserDTO>> SignInAsync(LoginViewModel login);

        public Task<Result<ApplicationUserDTO>> RegisterAsync(UserSaveViewModel saveUser);

        public Task<Result<Unit>> ConfirmAccountAsync(ConfirmAccountViewModel confirmAccount);

        public Task SignOutAsync();

        public Task<Result<Unit>> ForgotPasswordAsync(ForgotPasswordViewModel forgotPassword);

        public Task<Result<Unit>> ResetPasswordAsync(ResetPasswordViewModel resetPassword);
    }
}
