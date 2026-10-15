using System.Security.Claims;
using System.Text;
using DeliveryService.Application;
using DeliveryService.Infrastructure;
using DeliveryService.Infrastructure.Providers;
using DeliveryService.Persistence.Postgres;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryService.Web.Extensions
{
    public static class WebExtensions
    {
        public static void AddCorsConfiguration(this WebApplicationBuilder builder)
        {
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                ?? throw new InvalidOperationException("AllowedOrigins not configured.");

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        public static void AddJwtConfiguration(this WebApplicationBuilder builder)
        {
            var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions))
                ?? throw new InvalidOperationException("JwtOptions not configured.");

            builder.Services.Configure<JwtOptions>(jwtOptions);
        }

        public static void AddDatabasePostgres(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString(nameof(DeliveryServiceDbContext))
                ?? throw new InvalidOperationException($"ConnectionStrings not configured.");

            builder.Services.AddPersistencePostgresLayer(connectionString);
        }

        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructureLayer();
        }

        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationLayer();
        }

        public static void AddWebAuthentification(this WebApplicationBuilder builder)
        {
            var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()
                ?? throw new InvalidOperationException("JwtOptions not configured.");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

            builder.Services.AddAuthorization();
        }

        public static Guid GetCurrentUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User Id claim not found.");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
