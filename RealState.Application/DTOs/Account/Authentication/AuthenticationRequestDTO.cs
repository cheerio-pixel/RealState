namespace RealState.Application.DTOs.Account.Authentication
{
    public class AuthenticationRequestDTO
    {
        public string Account { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
