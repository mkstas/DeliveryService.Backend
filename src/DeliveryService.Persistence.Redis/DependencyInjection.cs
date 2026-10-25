using DeliveryService.Domain.Interfaces.Persistence.Redis;
using DeliveryService.Persistence.Redis.Repositories;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DeliveryService.Persistence.Redis
{
    /// <summary>
    /// Provides dependency injection extensions for the Redis persistence layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers Redis persistence dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the Redis persistence dependencies.</param>
        /// <param name="connectionString">The Redis connection string.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddPersistenceRedisLayer(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString));
            services.AddScoped(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
            services.AddScoped<IVerificationCodeStore, VerificationCodeStore>();

            return services;
        }
    }
}
