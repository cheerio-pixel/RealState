using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using RealState.Application.Interfaces.Services;
using RealState.Domain.Settings;
using RealState.Infrastructure.Shared.Services;

namespace RealState.Infrastructure.Shared
{
    public static class ServicesRegistration
    {
        public static void AddSharedLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Settings
            services.Configure<EmailSettings>(provider => configuration.GetSection("EmailSettings").Bind(provider));
            #endregion

            #region services
            services.AddSingleton<IUriServices>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var origin = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriServices(origin);
            });
            services.AddTransient<IEmailServices, EmailServices>();
            #endregion
        }
    }
}
