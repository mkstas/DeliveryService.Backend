using System.Text.Json;
using Application.Abstractions.Cache;
using StackExchange.Redis;

namespace Infrastructure.CacheService
{
    /// <summary>
    /// Implementation of <see cref="ICacheService"/>.
    /// </summary>
    internal class CacheService(IConnectionMultiplexer connection) : ICacheService
    {
        private readonly IDatabase _database = connection.GetDatabase();

        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(15);

        /// <inheritdoc />
        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);

            return value.IsNullOrEmpty
                ? default
                : JsonSerializer.Deserialize<T>((string)value!);
        }

        /// <inheritdoc />
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var json = JsonSerializer.Serialize(value);

            await _database.StringSetAsync(key, json, expiration ?? _defaultExpiration);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
