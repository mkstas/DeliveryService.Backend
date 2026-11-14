using Application.Abstractions.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.EmailService
{
    /// <summary>
    /// Implementation of <see cref="IEmailService"/>.
    /// </summary>
    internal class EmailService(IOptions<SmtpOptions> options) : IEmailService
    {
        private readonly SmtpOptions _options = options.Value;

        /// <inheritdoc />
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(_options.From));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = body
            };

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            await client.ConnectAsync(_options.Host, _options.Port, false);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
