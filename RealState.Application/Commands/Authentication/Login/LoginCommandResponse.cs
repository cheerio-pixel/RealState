using RealState.Application.DTOs.User;

namespace RealState.Application.Commands.Authentication.Login
{
    public class LoginCommandResponse
    {
        public ApplicationUserDTO CurrentUser { get; set; } = null!;
        public string JWToken { get; set; } = null!;
    }
}