using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using RealState.Application.Behaviours;


namespace RealState.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // Registrar MediatR

            services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });




            #region CommandValidators

            #endregion

        }
    }
}
