using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DeliveryService.Infrastructure.Email
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="options"></param>
    public class EmailSender(IOptions<SmtpOptions> options) : IEmailSender
    {
        private readonly SmtpOptions _options = options.Value;

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
