namespace RealState.Application.DTOs.Account.ConfirmAccount
{
    public class ConfirmAccountRequestDTO
    {
        public string UserID { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
