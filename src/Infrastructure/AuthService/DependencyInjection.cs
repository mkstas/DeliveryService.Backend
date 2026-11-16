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
            var jwtOptions = configuration.GetSection(nameof(AuthorizationJwtOptions)).Get<AuthorizationJwtOptions>()
                ?? throw new InvalidOperationException($"{nameof(AuthorizationJwtOptions)} not configured.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
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

            services.Configure<AuthorizationJwtOptions>(configuration.GetSection(nameof(AuthorizationJwtOptions)));
            services.AddSingleton<IAuthorizationJwtService, AuthorizationJwtService>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            return services;
        }
    }
}
