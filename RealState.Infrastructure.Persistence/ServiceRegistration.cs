using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                           b => b.MigrationsAssembly(typeof(MainContext).Assembly.FullName)));
        }
    }
}
