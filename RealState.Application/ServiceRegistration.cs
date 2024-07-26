﻿using System.Reflection;

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

            services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));

            #region CommandValidators

            #endregion
            services.AddAutoMapper([typeof(ServiceRegistration).Assembly]);
            services.AddValidatorsFromAssemblies([typeof(ServiceRegistration).Assembly]);

            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>))
                    .AddTransient<IPropertyTypeService, PropertyTypeService>()
                    .AddTransient<IAccountServices, AccountServices>()
                    .AddTransient<IRoleServices, RoleServices>();
            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IPropertyService, PropertyService>(); 
            // services.AddTransient<IPropertyTypeService, PropertyTypeService>();

        }
    }
}
