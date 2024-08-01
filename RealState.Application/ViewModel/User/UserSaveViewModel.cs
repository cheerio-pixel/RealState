using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace RealState.Application.ViewModel.User
{
    public class UserSaveViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(50, ErrorMessage = "User name can't be longer than 50 characters.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        public string? Picture { get; set; } = string.Empty;

        [Required(ErrorMessage = "Identifier card is required.")]
        [StringLength(20, ErrorMessage = "Identifier card can't be longer than 20 characters.")]
        public string IdentifierCard { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; } 

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; } 

        [Required(ErrorMessage = "At least one role is required.")]
        public List<string> RolesId { get; set; } = new List<string>();

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
