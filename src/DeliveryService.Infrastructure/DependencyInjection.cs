using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Infrastructure.Email;
using DeliveryService.Infrastructure.RustFS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Infrastructure
{
    /// <summary>
    /// Provides dependency injection extensions for the infrastructure layer.
    /// </summary>
    public static class DependencyInjection
    {
        // <summary>
        /// Registers infrastructure dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the infrastructure dependencies.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationJwtProvider, AuthorizationJwtProvider>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<IRustFSService, RustFSService>();

            return services;
        }
    }
}
