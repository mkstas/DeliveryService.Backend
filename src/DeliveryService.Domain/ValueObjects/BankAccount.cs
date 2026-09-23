using DeliveryService.Domain.Common.Helpers;

namespace DeliveryService.Domain.ValueObjects
{
    /// <summary>
    /// Value object that constrains a bank account.
    /// </summary>
    public record BankAccount
    {
        /// <summary>
        /// The required length of the bank account.
        /// </summary>
        public const int LENGTH = 20;

        private string Value { get; init; }

        private BankAccount(string value) => Value = value;

        /// <summary>
        /// Creates a <see cref="BankAccount"/> instance with validation.
        /// </summary>
        /// <param name="value">Must not be null, empty, and must consist of exactly <see cref="LENGTH"/> digits.</param>
        /// <returns>A validated <see cref="BankAccount"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the value is not exactly <see cref="LENGTH"/> characters long or contains non-digit characters.</exception>
        public static BankAccount Create(string value)
        {
            ValidationHelper.CheckNotNullOrEmpty(value, nameof(BankAccount));
            RegexValidationHelper.CheckStringOnlyDigits(value, nameof(BankAccount));
            ValidationHelper.CheckEqualToNumber(value.Length, LENGTH, nameof(BankAccount));

            return new BankAccount(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="BankAccount"/> to a <see cref="string"/>.
        /// </summary>
        public static implicit operator string(BankAccount bankAccount) => bankAccount.Value;
    }
}
