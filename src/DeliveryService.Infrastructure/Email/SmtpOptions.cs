namespace DeliveryService.Infrastructure.Email
{
    /// <summary>
    /// Represents SMTP configuration options for sending emails.
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        ///
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public int Port { get; set; } = 1025;

        /// <summary>
        ///
        /// </summary>
        public string From { get; set; } = string.Empty;
    }
}
