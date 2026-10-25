using System.Security.Claims;
using System.Text;
using Amazon.S3;
using DeliveryService.Application;
using DeliveryService.Infrastructure;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Infrastructure.Common;
using DeliveryService.Infrastructure.Email;
using DeliveryService.Infrastructure.RustFS;
using DeliveryService.Persistence.Postgres;
using DeliveryService.Persistence.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryService.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring the web application builder.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Registers the OpenAPI documentation services.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddDocumentaion(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenApi();
        }

        /// <summary>
        /// Configures controller routing with the prefix and lowercase URLs.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddRouteConvention(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new RoutePrefixConvention("api/v1"));
            });

            builder.Services.AddRouting(options => options.LowercaseUrls = true);
        }

        /// <summary>
        /// Configures the CORS policy using the origins from the <c>AllowedOrigins</c> configuration section.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
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

        /// <summary>
        /// Binds the <see cref="AuthorizationJwtOptions"/> from configuration.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddJwtConfiguration(this WebApplicationBuilder builder)
        {
            var jwtOptions = builder.Configuration.GetSection(nameof(AuthorizationJwtOptions))
                ?? throw new InvalidOperationException($"{nameof(AuthorizationJwtOptions)} not configured.");

            builder.Services.Configure<AuthorizationJwtOptions>(jwtOptions);
        }

        /// <summary>
        /// Binds the <see cref="SmtpOptions"/> from configuration.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddSmtpConfiguration(this WebApplicationBuilder builder)
        {
            var smtpOptions = builder.Configuration.GetSection(nameof(SmtpOptions))
                ?? throw new InvalidOperationException($"{nameof(SmtpOptions)} not configured.");

            builder.Services.Configure<SmtpOptions>(smtpOptions);
        }

        /// <summary>
        /// Configures JWT bearer authentication and authorization, reading the token from the cookie.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddAuthentification(this WebApplicationBuilder builder)
        {
            var jwtOptions = builder.Configuration.GetSection(nameof(AuthorizationJwtOptions)).Get<AuthorizationJwtOptions>()
                ?? throw new InvalidOperationException($"{nameof(AuthorizationJwtOptions)} not configured.");

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

        /// <summary>
        /// Gets the current user identifier from the <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="principal">The claims principal representing the current user.</param>
        /// <returns>The user identifier parsed from the name identifier claim.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the name identifier claim is missing.</exception>
        public static Guid GetCurrentUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User Id claim not found.");

            return Guid.Parse(userIdClaim.Value);
        }

        /// <summary>
        /// Binds the <see cref="RustFSOptions"/> from configuration and registers the S3 client for RustFS.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        /// <exception cref="InvalidOperationException">Thrown when the <c>RustFSOptions</c> configuration section is missing.</exception>
        public static void AddRustFSClient(this WebApplicationBuilder builder)
        {
            var rustFSoptions = builder.Configuration.GetSection(nameof(RustFSOptions))
                ?? throw new InvalidOperationException($"{nameof(RustFSOptions)} not configured.");

            builder.Services.Configure<RustFSOptions>(rustFSoptions);

            builder.Services.AddSingleton<IAmazonS3>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<RustFSOptions>>().Value;

                return new AmazonS3Client(
                    options.AccessKey,
                    options.SecretKey,
                    new AmazonS3Config
                    {
                        ServiceURL = options.ServiceURL,
                        ForcePathStyle = true
                    });
            });
        }

        /// <summary>
        /// Registers the Postgres persistense layer dependencies.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddPostgresPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString(nameof(DeliveryServiceDbContext))
                ?? throw new InvalidOperationException($"{nameof(DeliveryServiceDbContext)} connection string not configured.");

            builder.Services.AddPersistencePostgresLayer(connectionString);
        }

        /// <summary>
        /// Registers the Redis persistence layer dependencies.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddRedisPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException($"Redis connection string not configured.");

            builder.Services.AddPersistenceRedisLayer(connectionString);
        }

        /// <summary>
        /// Registers the application layer dependencies.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationLayer();
        }

        /// <summary>
        /// Registers the infrastructure layer dependencies.
        /// </summary>
        /// <param name="builder">The web application builder to configure.</param>
        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructureLayer();
        }
    }
}
