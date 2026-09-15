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
        /// Validates that a specified numeric value does not exceed the given upper limit.
        /// </summary>
        /// <param name="value">The numeric value to validate.</param>
        /// <param name="limit">The maximum allowed limit.</param>
        /// <param name="paramName">The name of the parameter being validated, used in the exception message.</param>
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
    }
}
