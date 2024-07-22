using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RealState.Domain.Settings;

namespace RealState.Infrastructure.Shared
{
    public static class ServicesRegistration
    {
        public static void AddSharedLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Settings
            services.Configure<EmailSettings>(provider => configuration.GetSection("EmailSettings").Bind(provider));
            #endregion

            #region
            #endregion
        }
    }
}
