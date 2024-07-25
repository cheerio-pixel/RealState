namespace RealState.Application.DTOs.Account.ResetPassword
{
    public class ResetPasswordRequestDTO
    {
        public string UserID { get; set; } = null!;

        public string Token { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
