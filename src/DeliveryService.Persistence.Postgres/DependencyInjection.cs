using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Persistence.Postgres
{
    /// <summary>
    /// Provides dependency injection extensions for the PostgreSQL persistence layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers PostgreSQL persistence dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the PostgreSQL persistence dependencies.</param>
        /// <param name="connectionString">The PostgreSQL connection string.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddPersistencePostgres(
            this IServiceCollection services,
            string connectionString
        )
        {
            services.AddDbContext<DeliveryServiceDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
