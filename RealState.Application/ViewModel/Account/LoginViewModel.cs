using System.ComponentModel.DataAnnotations;

namespace RealState.Application.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Account is required.")]
        public string Account { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
        public string? ReturnUrl { get; set; }
    }
}
