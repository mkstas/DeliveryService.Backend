using DeliveryService.Application.Services;
using DeliveryService.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Application
{
   /// <summary>
    /// Provides dependency injection extensions for the application layer.
    /// </summary>
    public static class DependencyInjection
    {
       // <summary>
        /// Registers application dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the application dependencies.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }
}
