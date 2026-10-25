using DeliveryService.Application.Services;
using DeliveryService.Domain.Interfaces.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Application
{
    /// <summary>
    /// Provides extension methods for registering application layer dependencies.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the application dependencies.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailVerificationService, EmailVerificationService>();

            return services;
        }
    }
}
