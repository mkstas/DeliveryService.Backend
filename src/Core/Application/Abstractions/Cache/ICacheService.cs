namespace Application.Abstractions.Cache
{
    /// <summary>
    /// Provides read/write access for the cache.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        Task RemoveAsync(string key);
    }
}
