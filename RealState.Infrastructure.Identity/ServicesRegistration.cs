using System.Net;
using System.Text;

using Asp.Versioning;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        public static IServiceCollection AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
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
               options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
               options.Lockout.MaxFailedAccessAttempts = 3;
               options.Lockout.AllowedForNewUsers = true;
           });
            #endregion

            #region services
            services.AddTransient<IIdentityServices, IdentityServices>();
            #endregion

            #region repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            #endregion

            #region options
            services.Configure<JWTSettings>(provider => configuration.GetSection("JwtSettings").Bind(provider));
            #endregion

            return services;
        }

        public static IServiceCollection AddJWTokenConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            #region Json Web Token
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
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

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
             {
                 swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                 {
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                 });
                 swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                             {
                                   new OpenApiSecurityScheme
                                     {
                                         Reference = new OpenApiReference
                                         {
                                             Type = ReferenceType.SecurityScheme,
                                             Id = "Bearer"
                                         }
                                     },
                                     new string[] {}
                             }
                 });
                 swagger.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Title = "Restaurant ITLA API",
                     Version = "v1"
                 });
             });

            // https://stackoverflow.com/questions/76371992/which-package-should-be-used-for-versioning-api-controllers-in-net-7-microsoft
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static IServiceCollection AddCookieConfigurations(this IServiceCollection services)
        {
            #region cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            return services;
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
