using RealState.Application.Enums;

namespace RealState.Application.QueryFilters.User
{
    public class UserQueryFilter
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string IdentifierCard { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public RoleTypes Role { get; set; }
    }
}
