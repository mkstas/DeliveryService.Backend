using DeliveryService.Domain.Common.Helpers;

namespace DeliveryService.Domain.ValueObjects
{
    /// <summary>
    /// Value object that constrains a string.
    /// </summary>
    public record StringUnbounded
    {
        private string Value { get; init; }

        private StringUnbounded(string value) => Value = value;

        /// <summary>
        /// Creates a <see cref="StringUnbounded"/> instance with validation.
        /// </summary>
        /// <param name="value">Must not be null or empty.</param>
        /// <returns>A validated <see cref="StringUnbounded"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is null or empty.</exception>
        public static StringUnbounded Create(string value)
        {
            ValidationHelper.CheckNotNullOrEmpty(value, nameof(value));

            return new StringUnbounded(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="StringUnbounded"/> to a <see cref="string"/>.
        /// </summary>
        public static implicit operator string(StringUnbounded stringUnbounded) => stringUnbounded.Value;
    }
}
