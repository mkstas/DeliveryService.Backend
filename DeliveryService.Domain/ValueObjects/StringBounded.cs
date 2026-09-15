using DeliveryService.Domain.Common.Helpers;

namespace DeliveryService.Domain.ValueObjects
{
    /// <summary>
    /// Value object that constrains a string to a maximum length.
    /// </summary>
    public record StringBounded
    {
        /// <summary>
        /// The maximum allowed length of the string.
        /// </summary>
        public const int MAX_LENGTH = 256;

        private string Value { get; init; }

        private StringBounded(string value) => Value = value;

        /// <summary>
        /// Creates a <see cref="StringBounded"/> instance with validation.
        /// </summary>
        /// <param name="value">Must not be null, empty, or longer than <see cref="MAX_LENGTH"/>.</param>
        /// <returns>A validated <see cref="StringBounded"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value exceeds <see cref="MAX_LENGTH"/>.</exception>
        public static StringBounded Create(string value)
        {
            ValidationHelper.CheckNotNullOrEmpty(value, nameof(value));
            ValidationHelper.CheckGreaterThanLimit(value.Length, MAX_LENGTH, nameof(StringBounded));

            return new StringBounded(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="StringBounded"/> to a <see cref="string"/>.
        /// </summary>
        public static implicit operator string(StringBounded stringBounded) => stringBounded.Value;
    }
}
