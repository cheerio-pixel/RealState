using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Infrastructure.Identity.Context;
using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Repositories;
using RealState.Infrastructure.Identity.Seeds;
using RealState.Infrastructure.Identity.Services;


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

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            #region services
            services.AddTransient<IAccountServices, AccountServices>();
            #endregion

            #region repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            #endregion
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
