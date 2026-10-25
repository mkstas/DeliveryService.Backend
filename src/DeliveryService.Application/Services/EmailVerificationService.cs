using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Domain.Interfaces.Persistence.Redis;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Email;
using System.Security.Cryptography;

namespace DeliveryService.Application.Services
{
    /// <summary>
    /// Provides email verification functionality.
    /// </summary>
    public class EmailVerificationService(
        IUserRepository userRepository,
        IVerificationCodeStore verificationCodeStore,
        IEmailSender emailSender) : IEmailVerificationService
    {
        private static readonly TimeSpan ttl = TimeSpan.FromMinutes(10);

        public async Task SendConfirmationCodeAsync(EmailAddress address)
        {
            var user = await userRepository.GetUserByEmailAsync(address);

            if (user is null || user.VerifiedAt is not null)
            {
                return;
            }

            var code = GenerateCode();

            await verificationCodeStore.SetCodeAsync(address, code, ttl);

            string subject = "Подтверждение элекронной почты Delivery Service.";
            string body = $"Код подтверждения: {code}. Код действителен в течение {ttl.TotalMinutes} минут.";

            await emailSender.SendEmailAsync(address, subject, body);
        }

        public async Task ConfirmEmailAsync(EmailAddress address, string code)
        {
            var user = await userRepository.GetUserByEmailAsync(address) ?? throw new NotFoundException<User>(address);

            if (user.VerifiedAt is not null)
            {
                await verificationCodeStore.RemoveCodeAsync(address);
                return;
            }

            var storedCode = await verificationCodeStore.GetCodeAsync(address);

            if (string.IsNullOrWhiteSpace(storedCode) || !string.Equals(storedCode.Trim(), code.Trim(), StringComparison.Ordinal))
            {
                throw new UnauthorizedAccessException("Invalid or expired verification code.");
            }

            user.ConfirmEmailAddress();

            await userRepository.UpdateUserAsync(user);
            await verificationCodeStore.RemoveCodeAsync(address);
        }

        private static string GenerateCode()
        {
            var bytes = RandomNumberGenerator.GetBytes(4);
            var value = BitConverter.ToUInt32(bytes, 0) % 1000000;

            return value.ToString("D6");
        }
    }
}
