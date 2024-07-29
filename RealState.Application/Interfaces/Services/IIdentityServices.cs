using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.DTOs.Account.ConfirmAccount;
using RealState.Application.DTOs.Account.ForgotPassword;
using RealState.Application.DTOs.Account.RegisterResponse;
using RealState.Application.DTOs.Account.ResetPassword;
using RealState.Application.DTOs.User;

namespace RealState.Application.Interfaces.Services
{
    public interface IIdentityServices
    {
        public Task<AuthenticationResponseDTO> AuthenticationAsync(AuthenticationRequestDTO request);

        public Task<RegisterResponseDTO> RegisterAsync(SaveApplicationUserDTO saveUser);

        public Task<ConfirmAccountResponseDTO> ConfirmAccountAsync(ConfirmAccountRequestDTO request);

        public Task SignOutAsync();

        public Task<ForgotPasswordResponseDTO> ForgotPasswordAsync(ForgotPasswordRequestDTO request);

        public Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request);
    }
}
