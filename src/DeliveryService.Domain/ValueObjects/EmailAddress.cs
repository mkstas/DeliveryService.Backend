using System.Net.Mail;
using DeliveryService.Domain.Common.Helpers;

namespace DeliveryService.Domain.ValueObjects
{
    /// <summary>
    /// Value object that constrains an email address.
    /// </summary>
    public record EmailAddress
    {
        /// <summary>
        /// The maximum allowed length of the email address.
        /// </summary>
        public const int MAX_LENGTH = 256;

        private string Value { get; init; }

        private EmailAddress(string value) => Value = value;

        /// <summary>
        /// Creates a <see cref="EmailAddress"/> instance with validation.
        /// </summary>
        /// <param name="value">Must not be null, empty, or exceed <see cref="MAX_LENGTH"/> characters.</param>
        /// <returns>A validated <see cref="EmailAddress"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value exceeds <see cref="MAX_LENGTH"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when the value is not a valid email address.</exception>
        public static EmailAddress Create(string value)
        {
            ValidationHelper.CheckNotNullOrEmpty(value, nameof(EmailAddress));
            ValidationHelper.CheckGreaterThanLimit(value.Length, MAX_LENGTH, nameof(EmailAddress));

            try
            {
                _ = new MailAddress(value);
                return new EmailAddress(value);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid email address format.", nameof(value));
            }
        }

        /// <summary>
        /// Implicitly converts an <see cref="EmailAddress"/> to a <see cref="string"/>.
        /// </summary>
        public static implicit operator string(EmailAddress emailAddress) => emailAddress.Value;
    }
}
