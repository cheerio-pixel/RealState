using Microsoft.AspNetCore.Identity;
using RealState.Application.Enums;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task CreateSeed(RoleManager<ApplicationRole> roleManager)
        {
            #region Admin
            var adminExists = await roleManager.RoleExistsAsync(RoleTypes.Admin.ToString());
            if (!adminExists)
                await roleManager.CreateAsync(new()
                {
                    Name = RoleTypes.Admin.ToString(),
                    NormalizedName = RoleTypes.Admin.ToString().ToUpper()
                });
            #endregion

            #region Client
            var clientExists = await roleManager.RoleExistsAsync(RoleTypes.Client.ToString());
            if (!clientExists)
                await roleManager.CreateAsync(new()
                {
                    Name = RoleTypes.Client.ToString(),
                    NormalizedName = RoleTypes.Client.ToString().ToUpper()
                });
            #endregion

            #region StateAgent
            var stateAgentExists = await roleManager.RoleExistsAsync(RoleTypes.StateAgent.ToString());
            if (!stateAgentExists)
                await roleManager.CreateAsync(new()
                {
                    Name = RoleTypes.StateAgent.ToString(),
                    NormalizedName = RoleTypes.StateAgent.ToString().ToUpper()
                });
            #endregion

            #region Developer
            var developerExists = await roleManager.RoleExistsAsync(RoleTypes.Developer.ToString());
            if (!developerExists)
                await roleManager.CreateAsync(new()
                {
                    Name = RoleTypes.Developer.ToString(),
                    NormalizedName = RoleTypes.Developer.ToString().ToUpper()
                });
            #endregion
        }
    }
}
