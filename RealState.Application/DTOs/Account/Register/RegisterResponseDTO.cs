namespace RealState.Application.DTOs.Account.RegisterResponse
{
    public class RegisterResponseDTO
    {
        public bool Success { get; set; }

        public string Error { get; set; } = null!;
    }
}
