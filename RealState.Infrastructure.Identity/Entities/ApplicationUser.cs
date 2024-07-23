using Microsoft.AspNetCore.Identity;

namespace RealState.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public string IdentifierCard { get; set; } = null!;

        // Navigators
        public ICollection<ApplicationRole> Roles { get; set; } = [];
    }
}
