namespace DeliveryService.Domain.Interfaces.Redis
{
    /// <summary>
    /// Stores and validates email verification codes.
    /// </summary>
    public interface IVerificationCodeStore
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        Task SetCodeAsync(string email, string code, TimeSpan ttl);

        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string?> GetCodeAsync(string email);

        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task RemoveCodeAsync(string email);
    }
}
