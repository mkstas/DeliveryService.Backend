namespace DeliveryService.Domain.Common.Helpers
{
    public static partial class ValidationHelper
    {
        /// <summary>
        /// Validates that the specified string is neither <see langword="null"/> nor empty.
        /// </summary>
        /// <param name="value">The string value to check.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <see langword="null"/> or empty (<c>""</c>).
        /// </exception>
        public static void CheckNotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null or empty.");
            }
        }

        /// <summary>
        /// Validates that the specified numeric value does not exceed the given upper limit.
        /// </summary>
        /// <param name="value">The numeric value to validate.</param>
        /// <param name="limit">The maximum allowed limit.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is strictly greater than <paramref name="limit"/>.
        /// </exception>
        public static void CheckGreaterThanLimit(double value, double limit, string paramName)
        {
            if (value > limit)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be greater than {limit}.");
            }
        }

        /// <summary>
        /// Validates that the specified numeric value is equal to the given number.
        /// </summary>
        /// <param name="value">The numeric value to validate.</param>
        /// <param name="number">The exact value the <paramref name="value"/> must equal.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is not equal to <paramref name="number"/>.
        /// </exception>
        public static void CheckEqualToNumber(double value, double number, string paramName)
        {
            if (value != number)
            {
                throw new ArgumentException($"{paramName} must be equal {number}.");
            }
        }

        /// <summary>
        /// Validates that the specified decimal value is not negative.
        /// </summary>
        /// <param name="value">The decimal value to validate.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is negative.
        /// </exception>
        public static void CheckIsPositive(decimal value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be negative.");
            }
        }
    }
}
