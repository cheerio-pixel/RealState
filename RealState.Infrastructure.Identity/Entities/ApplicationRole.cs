using Microsoft.AspNetCore.Identity;

namespace RealState.Infrastructure.Identity.Entities
{
    public class ApplicationRole : IdentityRole
    {
        // Navigators
        public ICollection<ApplicationUser> Users { get; set; } = [];
    }
}
