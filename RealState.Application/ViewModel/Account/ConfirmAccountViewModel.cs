using System.ComponentModel.DataAnnotations;

namespace RealState.Application.ViewModel.Account
{
    public class ConfirmAccountViewModel
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; } = string.Empty;
    }
}
