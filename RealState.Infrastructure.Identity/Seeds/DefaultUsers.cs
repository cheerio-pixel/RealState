using Microsoft.AspNetCore.Identity;

using RealState.Application.Enums;
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
                NormalizedUserName = "DEFAULTADMIN",
                IdentifierCard = "0",
                Picture = ""
            };

            var adminByName = await userManager.FindByNameAsync(admin.UserName);
            if (adminByName is not null) return;

            var adminByEmail = await userManager.FindByEmailAsync(admin.Email);
            if (adminByEmail is not null) return;

            var result = await userManager.CreateAsync(admin, "123Pa$$word!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, nameof(RoleTypes.Admin));
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
                NormalizedUserName = "DEFAULTCLIENT",
                IdentifierCard = "00",
                Picture = ""
            };

            var clientByName = await userManager.FindByNameAsync(client.UserName);
            if (clientByName is not null) return;

            var clientByEmail = await userManager.FindByEmailAsync(client.Email);
            if (clientByEmail is not null) return;

            var result = await userManager.CreateAsync(client, "123Pa$$word!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(client, nameof(RoleTypes.Client));
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
                NormalizedUserName = "DEFAULTSTATUSAGENT",
                IdentifierCard = "000",
                Picture = ""
            };

            var statusAgentByName = await userManager.FindByNameAsync(statusAgent.UserName);
            if (statusAgentByName is not null) return;

            var statusAgentByEmail = await userManager.FindByEmailAsync(statusAgent.Email);
            if (statusAgentByEmail is not null) return;

            var result = await userManager.CreateAsync(statusAgent, "123Pa$$word!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(statusAgent, nameof(RoleTypes.StateAgent));
            }
        }
    }
}
