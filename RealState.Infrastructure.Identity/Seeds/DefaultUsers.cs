using Microsoft.AspNetCore.Identity;

using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Seeds
{
    public static class DefaultUsers
    {
        public static async Task CreateAdminSeed(UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser()
            {
                FirstName = "Jhone",
                LastName = "Dou",
                UserName = "DefaultAdmin",
                Email = "DefaultAdmin@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "DEFAULTADMIN@GMAIL.COM",
                NormalizedUserName = "DEFAULTADMIN"
            };

            var adminByName = await userManager.FindByNameAsync(admin.UserName);
            if (adminByName is null) return;

            var adminByEmail = await userManager.FindByEmailAsync(admin.Email);
            if (adminByEmail is null) return;

            try
            {
                var result = await userManager.CreateAsync(admin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task CreateClientSeed(UserManager<ApplicationUser> userManager)
        {
            var client = new ApplicationUser()
            {
                FirstName = "Jane",
                LastName = "Dou",
                UserName = "DefaultClient",
                Email = "DefaultClient@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "DEFAULTCLIENT@GMAIL.COM",
                NormalizedUserName = "DEFAULTCLIENT"
            };

            var clientByName = await userManager.FindByNameAsync(client.UserName);
            if (clientByName is null) return;

            var clientByEmail = await userManager.FindByEmailAsync(client.Email);
            if (clientByEmail is null) return;

            try
            {
                var result = await userManager.CreateAsync(client);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task CreateStateAgentSeed(UserManager<ApplicationUser> userManager)
        {
            var statusAgent = new ApplicationUser()
            {
                FirstName = "Juan",
                LastName = "Garcia",
                UserName = "DefaultStatusAgent",
                Email = "DefaultStatusAgent@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "DEFAULTSTATUSAGENT@GMAIL.COM",
                NormalizedUserName = "DEFAULTSTATUSAGENT"
            };

            var statusAgentByName = await userManager.FindByNameAsync(statusAgent.UserName);
            if (statusAgentByName is null) return;

            var statusAgentByEmail = await userManager.FindByEmailAsync(statusAgent.Email);
            if (statusAgentByEmail is null) return;

            try
            {
                var result = await userManager.CreateAsync(statusAgent);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
