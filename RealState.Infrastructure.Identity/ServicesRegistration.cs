using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Seeds;


namespace RealState.Infrastructure.Identity
{
    public static class ServicesRegistration
    {
        public static void AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("ConnectionStrings"),
                m=>m.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)
                ));
        }

        public static async Task RunSeedsAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var service = scope.ServiceProvider;

            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = service.GetRequiredService<RoleManager<ApplicationRole>>();

            await DefaultRoles.CreateSeed(roleManager);
            await DefaultUsers.CreateAdminSeed(userManager);
            await DefaultUsers.CreateClientSeed(userManager);
            await DefaultUsers.CreateStateAgentSeed(userManager);

        }
    }
}
