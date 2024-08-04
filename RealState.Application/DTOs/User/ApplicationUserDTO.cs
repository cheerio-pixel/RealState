using RealState.Application.DTOs.Role;

namespace RealState.Application.DTOs.User
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public string IdentifierCard { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public bool Active { get; set; }

        public ICollection<ApplicationRoleDTO> Roles { get; set; } = [];
    }

}
