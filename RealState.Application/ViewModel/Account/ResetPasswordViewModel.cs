namespace RealState.Application.ViewModel.Account
{
    public class ResetPasswordViewModel
    {
        public required Guid UserId { get; set; }

        public required string Token { get; set; }

        public required string Password { get; set; }

        public required string ConfirmPassword { get; set; }
    }
}
