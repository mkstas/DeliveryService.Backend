namespace DeliveryService.Domain.Interfaces.Persistence.Redis
{
    /// <summary>
    /// Stores and validates email verification codes.
    /// </summary>
    public interface IVerificationCodeStore
    {
        /// <summary>
        /// Stores the verification code for the specified email address.
        /// </summary>
        /// <param name="email">The email address the verification code belongs to.</param>
        /// <param name="code">The verification code to store.</param>
        /// <param name="ttl">The time to live after which the code expires.</param>
        Task SetCodeAsync(string email, string code, TimeSpan ttl);

        /// <summary>
        /// Retrieves the verification code for the specified email address.
        /// </summary>
        /// <param name="email">The email address the verification code belongs to.</param>
        /// <returns>The verification code if one exists and has not expired; otherwise, <c>null</c>.</returns>
        Task<string?> GetCodeAsync(string email);

        /// <summary>
        /// Removes the verification code for the specified email address.
        /// </summary>
        /// <param name="email">The email address the verification code belongs to.</param>
        Task RemoveCodeAsync(string email);
    }
}
