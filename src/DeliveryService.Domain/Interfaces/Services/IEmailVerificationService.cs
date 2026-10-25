using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Services
{
    /// <summary>
    /// Provides email verification functionality.
    /// </summary>
    public interface IEmailVerificationService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task SendConfirmationCodeAsync(EmailAddress address);

        /// <summary>
        ///
        /// </summary>
        /// <param name="address"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task ConfirmEmailAsync(EmailAddress address, string code);
    }
}
