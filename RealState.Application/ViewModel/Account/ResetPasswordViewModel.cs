using System.ComponentModel.DataAnnotations;

namespace RealState.Application.ViewModel.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
