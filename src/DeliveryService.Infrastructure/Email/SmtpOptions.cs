namespace DeliveryService.Infrastructure.Email
{
    /// <summary>
    /// Represents SMTP configuration options for sending emails.
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// The host name or IP address of the SMTP server.
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// The port used to connect to the SMTP server.
        /// </summary>
        public int Port { get; set; } = 1025;

        /// <summary>
        /// The sender email address used.
        /// </summary>
        public string From { get; set; } = string.Empty;
    }
}
