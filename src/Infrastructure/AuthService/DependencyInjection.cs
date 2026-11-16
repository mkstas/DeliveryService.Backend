using System.Text;
using Application.Abstractions.Auth;
using Infrastructure.AuthService.Jwt;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.AuthService
{
    /// <summary>
    /// Provides dependency injection for the auth services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers auth dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the auth dependencies.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to get auth jwt options.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddAuthService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
                ?? throw new InvalidOperationException($"{nameof(JwtOptions)} not configured.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["access_token"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            return services;
        }
    }
}
