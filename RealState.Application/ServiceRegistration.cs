using FluentValidation;

using MediatR;

using Microsoft.Extensions.Configuration;
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
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper([typeof(ServiceRegistration).Assembly]);
            services.AddValidatorsFromAssemblies([typeof(ServiceRegistration).Assembly]);

            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>))
                    .AddTransient<IPropertyTypeService, PropertyTypeService>()
                    .AddTransient<IAccountServices, AccountServices>();
        }
    }
}
