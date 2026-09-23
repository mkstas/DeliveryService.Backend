using System.Text.RegularExpressions;

namespace DeliveryService.Domain.Common.Helpers
{
    /// <summary>
    /// Provides regular expression based validation helpers.
    /// </summary>
    public static partial class RegexValidationHelper
    {
        [GeneratedRegex(@"^\d+$", RegexOptions.Compiled)]
        private static partial Regex DigitsRegex();

        /// <summary>
        /// Validates that the specified string contains only digits.
        /// </summary>
        /// <param name="value">The string value to check.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> contains any non-digit character.
        /// </exception>
        public static void CheckStringOnlyDigits(string value, string paramName)
        {
            if (!DigitsRegex().IsMatch(value))
            {
                throw new ArgumentException($"{paramName} must contain only digits.", paramName);
            }
        }
    }
}
