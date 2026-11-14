namespace Application.Abstractions.Cache
{
    /// <summary>
    /// Provides read/write access for the cache.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Returns the cached value for the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the cached value.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <returns>The cached value, or <c>null</c> if the key does not exist.</returns>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// Stores the specified value in the cache.
        /// </summary>
        /// <typeparam name="T">The type of the value to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value to cache.</param>
        /// <param name="expiration">The expiration time; uses the default when not specified.</param>
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// Removes the cached value for the specified key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        Task RemoveAsync(string key);
    }
}
