using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    /// <summary>
    /// Provides dependency injection for the application services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the application dependencies.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddApplicationService(
            this IServiceCollection services)
        {
            return services;
        }
    }
}
