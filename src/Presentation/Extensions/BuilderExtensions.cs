using System.Security.Claims;

namespace Presentation.Extensions
{
    /// <summary>
    /// Provides extension methods for the service collection and current user.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Configures the CORS policy using the origins from the <c>AllowedOrigins</c> configuration section.
        /// </summary>
        /// <param name="services">The service collection to add CORS to.</param>
        /// <param name="configuration">The configuration containing allowed origins.</param>
        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()
                ?? throw new InvalidOperationException("AllowedOrigins not configured.");

            services.AddCors(options =>
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
        /// Gets the current user identifier from the <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="principal">The claims principal representing the current user.</param>
        /// <returns>The user identifier parsed from the name identifier claim.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the name identifier claim is missing.</exception>
        public static Guid GetCurrentId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User Id claim not found.");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
