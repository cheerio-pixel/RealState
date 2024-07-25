namespace RealState.Application.ViewModel.Account
{
    public class ConfirmAccountViewModel
    {
        public required Guid UserId { get; set; }

        public required string Token { get; set; }
    }
}
