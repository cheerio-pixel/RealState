using RealState.Application.DTOs.Role;

namespace RealState.Application.DTOs.User
{
    public class SaveApplicationUserDTO
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public string IdentifierCard { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
        
        public bool? Active { get; set; }

        public List<ApplicationRoleDTO> Roles { get; set; } = [];
    }
}
