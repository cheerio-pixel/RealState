using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using RealState.Application.Behaviours;
using RealState.Application.Interfaces.Services;
using RealState.Application.Services;


namespace RealState.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // Registrar MediatR


            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));

            #region CommandValidators
            services.AddValidatorsFromAssemblies([typeof(ServiceRegistration).Assembly]);

            #endregion
            services.AddAutoMapper([typeof(ServiceRegistration).Assembly]);

            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>))
                    .AddTransient<IPropertyTypeService, PropertyTypeService>()
                    .AddTransient<ISalesTypesService, SalesTypesService>()
                    .AddTransient<IUpgradesService, UpgradesService>()
                    .AddTransient<IAccountServices, AccountServices>()
                    .AddTransient<IRoleServices, RoleServices>()
                    .AddTransient<IPropertyService, PropertyService>()
                    .AddTransient<IPropertyUpgradeService, PropertyUpgradeService>()
                    .AddTransient<IPictureService, PictureService>()
                    .AddTransient<IUserServices, UserServices>()
                    .AddTransient<IFavoriteService, FavoriteService>();
        }
    }
}
