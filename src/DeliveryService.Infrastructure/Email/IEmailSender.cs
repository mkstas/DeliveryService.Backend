namespace DeliveryService.Infrastructure.Email
{
    /// <summary>
    /// Provides email sending functionality.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email message asynchronously.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task SendEmailAsync(string to, string subject, string body);
    }
}
