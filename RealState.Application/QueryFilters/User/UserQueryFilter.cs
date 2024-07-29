using RealState.Application.Enums;

namespace RealState.Application.QueryFilters.User
{
    public class UserQueryFilter
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? IdentifierCard { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? Active { get; set; }

        public RoleTypes? Role { get; set; }
    }
}
