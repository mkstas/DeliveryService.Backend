using System.Net.Mail;

namespace Domain.Helpers
{
    /// <summary>
    /// Provides common argument validation helpers.
    /// </summary>
    public static partial class ValidationHelper
    {
        /// <summary>
        /// Validates that the specified string is neither <c>null</c> nor empty.
        /// </summary>
        /// <param name="value">The string value to check.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c> or empty.
        /// </exception>
        public static void CheckNotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null or empty.");
            }
        }

        /// <summary>
        /// Validates that the specified string is in a correct email address format.
        /// </summary>
        /// <param name="value">The email address string to validate.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is not a valid email address format.
        /// </exception>
        public static void CheckCorrectEmailFormat(string value, string paramName)
        {
            try
            {
                _ = new MailAddress(value);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid email address format.", paramName);
            }
        }
    }
}
