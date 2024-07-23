using FluentValidation;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealState.Application.Behaviours;
using RealState.Application.Commands.Property.Create;

namespace RealState.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssemblies([typeof(ServiceRegistration).Assembly]);
            services.AddAutoMapper([typeof(ServiceRegistration).Assembly]);

            #region CommandValidators
            services.AddTransient<IValidator<CreatePropertyCommand>, CreatePropertyCommandValidator>();
            #endregion

        }
    }
}
