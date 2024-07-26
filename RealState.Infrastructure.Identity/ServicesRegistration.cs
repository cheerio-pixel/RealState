using System.Net;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Domain.Settings;
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
            #region Identity
            var sqlIdentityConnection = configuration.GetConnectionString("IdentityConnection")
                                        ?? throw new InvalidOperationException("The connection string is missing from the configuration.");

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(
                sqlIdentityConnection,
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)
            ));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
           {
               options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
               options.Lockout.MaxFailedAccessAttempts = 3;
               options.Lockout.AllowedForNewUsers = true;
           });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Authentication/Index";
                options.AccessDeniedPath = "/Authentication/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            #region services
            services.AddTransient<IIdentityServices, IdentityServices>();
            #endregion

            #region repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            #endregion

            #region Json Web Token
            //settings
            services.Configure<JWTSettings>(provider => configuration.GetSection("JwtSettings").Bind(provider));

            //configuration of jwt
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = (x) =>
                    {
                        x.NoResult();
                        x.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        x.Response.ContentType = "text/plain";
                        return x.Response.WriteAsync(x.Exception.ToString());
                    },

                    OnChallenge = (x) =>
                    {
                        x.HandleResponse();
                        x.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        x.Response.ContentType = "application/json";
                        var response = new { Success = false, Error = "You are not authenticated" };
                        return x.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    },
                    OnForbidden = (x) =>
                    {
                        x.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        x.Response.ContentType = "application/json";
                        var response = new { Success = false, Error = "You are not authorized" };
                        return x.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }
                };
            });
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
