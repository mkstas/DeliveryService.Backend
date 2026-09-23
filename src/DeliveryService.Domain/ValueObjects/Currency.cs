using DeliveryService.Domain.Common.Helpers;

namespace DeliveryService.Domain.ValueObjects
{
    /// <summary>
    /// Value object that constrains a monetary amount.
    /// </summary>
    public record Currency
    {
        /// <summary>
        /// The monetary amount.
        /// </summary>
        public decimal Value { get; init; }

        private Currency(decimal value) => Value = value;

        /// <summary>
        /// Creates a <see cref="Currency"/> instance with validation.
        /// </summary>
        /// <param name="value">Must not be negative.</param>
        /// <returns>A validated <see cref="Currency"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is negative.</exception>
        public static Currency Create(decimal value)
        {
            ValidationHelper.CheckIsPositive(value, nameof(Currency));

            return new Currency(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="Currency"/> to a <see cref="decimal"/>.
        /// </summary>
        public static implicit operator decimal(Currency currency) => currency.Value;
    }
}
