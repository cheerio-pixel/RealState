using System.ComponentModel.DataAnnotations;

namespace RealState.Application.ViewModel.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Account is required.")]
        public string Account { get; set; } = null!;
    }
}
