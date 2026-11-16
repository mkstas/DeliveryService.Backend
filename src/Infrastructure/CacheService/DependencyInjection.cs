using Application.Abstractions.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.CacheService
{
    /// <summary>
    /// Provides dependency injection for the cache services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers cache dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the cache dependencies.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to get connection string.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddCacheService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException($"Redis connection string not configured.");

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString));
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
