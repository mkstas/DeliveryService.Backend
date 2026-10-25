namespace DeliveryService.Infrastructure.Email
{
    /// <summary>
    /// Provides email sending functionality.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        /// <param name="to">The recipient email address.</param>
        /// <param name="subject">The subject of the message.</param>
        /// <param name="body">The HTML body of the message.</param>
        Task SendEmailAsync(string to, string subject, string body);
    }
}
