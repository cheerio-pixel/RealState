using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace RealState.Application.ViewModel.User
{
    public class AgentSaveViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string LastName { get; set; } = null!;
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }
        public IFormFile Picture { get; set; } = null!;
        public string? PictureUrl { get; set; } = string.Empty;

    }
}
