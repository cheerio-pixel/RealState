using RealState.Application.DTOs.User;

namespace RealState.Application.DTOs.Account.RegisterResponse
{
    public class RegisterResponseDTO
    {
        public ApplicationUserDTO? NewUser { get; set; }

        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}
