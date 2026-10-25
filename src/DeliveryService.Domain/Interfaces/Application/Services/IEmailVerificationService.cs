using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides email verification functionality.
    /// </summary>
    public interface IEmailVerificationService
    {
        /// <summary>
        /// Sends a confirmation code to the specified email address.
        /// </summary>
        /// <param name="address">The email address to send the confirmation code to.</param>
        Task SendConfirmationCodeAsync(EmailAddress address);

        /// <summary>
        /// Confirms the email address of the user associated with the specified email address.
        /// </summary>
        /// <param name="address">The email address to confirm.</param>
        /// <param name="code">The confirmation code sent to the email address.</param>
        Task ConfirmEmailAsync(EmailAddress address, string code);
    }
}
