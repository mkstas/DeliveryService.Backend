using DeliveryService.Domain.Common.Helpers;

namespace DeliveryService.Domain.ValueObjects
{
    /// <summary>
    /// Value object that constrains a card number.
    /// </summary>
    public record CardNumber
    {
        /// <summary>
        /// The required length of the card number.
        /// </summary>
        public const int LENGTH = 16;

        private string Value { get; init; }

        private CardNumber(string value) => Value = value;

        /// <summary>
        /// Creates a <see cref="CardNumber"/> instance with validation.
        /// </summary>
        /// <param name="value">Must not be null, empty, and must consist of exactly <see cref="LENGTH"/> digits; spaces and hyphens are removed.</param>
        /// <returns>A validated <see cref="CardNumber"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the value is not exactly <see cref="LENGTH"/> characters long or contains non-digit characters.</exception>
        public static CardNumber Create(string value)
        {
            ValidationHelper.CheckNotNullOrEmpty(value, nameof(CardNumber));

            value = value.Replace(" ", "").Replace("-", "");

            ValidationHelper.CheckEqualToNumber(value.Length, LENGTH, nameof(CardNumber));
            RegexValidationHelper.CheckStringOnlyDigits(value, nameof(CardNumber));

            return  new CardNumber(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="CardNumber"/> to a <see cref="string"/>.
        /// </summary>
        public static implicit operator string(CardNumber cardNumber) => cardNumber.Value;
    }
}
