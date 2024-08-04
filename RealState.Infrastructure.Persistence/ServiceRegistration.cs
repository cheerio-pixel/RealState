using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Application.Interfaces.Repositories;
using RealState.Infrastructure.Persistence.Context;
using RealState.Infrastructure.Persistence.Repositories;

namespace RealState.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                               b => b.MigrationsAssembly(typeof(MainContext).Assembly.FullName)));

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddTransient<ISalesTypeRepository, SalesTypeRepository>();
            services.AddTransient<IUpgradesRepository, UpgradesRepository>();
            services.AddTransient<IPictureRepository, PictureRepository>();
            services.AddTransient<IPropertyUpgradeRepository, PropertyUpgradeRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            #endregion
        }
    }
}
