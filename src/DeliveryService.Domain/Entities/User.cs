using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a user within the system.
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// The email address of the user.
        /// </summary>
        public EmailAddress EmailAddress { get; private set; }

        /// <summary>
        /// The password hash of the user.
        /// </summary>
        public StringUnbounded PasswordHash { get; private set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        public StringBounded FirstName { get; private set; }

        private readonly List<Role> _roles = [];
        /// <summary>
        /// The roles assigned to the user.
        /// </summary>
        public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

        private readonly List<PaymentMethod> _paymentMethods = [];
        /// <summary>
        /// The payment methods of the user.
        /// </summary>
        public IReadOnlyList<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

        private User(EmailAddress emailAddress, StringUnbounded passwordHash, StringBounded firstName)
        {
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
            FirstName = firstName;
        }

        /// <summary>
        /// Creates a new <see cref="User"/> instance.
        /// </summary>
        /// <param name="emailAddress">Must not be null, empty, or exceed <see cref="EmailAddress.MAX_LENGTH"/> characters.</param>
        /// <param name="passwordHash">Must not be null or empty.</param>
        /// <param name="firstName">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="User"/> instance.</returns>
        public static User Create(EmailAddress emailAddress, StringUnbounded passwordHash, StringBounded firstName)
            => new(emailAddress, passwordHash, firstName);

        /// <summary>
        /// Changes the email address of the user.
        /// </summary>
        /// <param name="newEmailAddress">The new email address to assign to the user.</param>
        public void ChangeEmailAddress(EmailAddress newEmailAddress) => EmailAddress = newEmailAddress;

        /// <summary>
        /// Changes the password hash of the user.
        /// </summary>
        /// <param name="newPasswordHash">The new password hash to assign to the user.</param>
        public void ChangePasswordHash(StringUnbounded newPasswordHash) => PasswordHash = newPasswordHash;

        /// <summary>
        /// Changes the first name of the user.
        /// </summary>
        /// <param name="newFirstName">The new first name to assign to the user.</param>
        public void ChangeFirstName(StringBounded newFirstName) => FirstName = newFirstName;

        /// <summary>
        /// Adds a role to the user.
        /// </summary>
        /// <param name="role">The role to add.</param>
        public void AddRole(Role role) => _roles.Add(role);

        /// <summary>
        /// Removes a role from the user.
        /// </summary>
        /// <param name="role">The role to remove.</param>
        public void RemoveRole(Role role) => _roles.Remove(role);

        /// <summary>
        /// Creates a payment method for the user from the given card number.
        /// </summary>
        /// <param name="cardNumber">The card number of the new payment method.</param>
        public void CreatePaymentMethod(CardNumber cardNumber)
        {
            var paymentMethod = PaymentMethod.Create(Id, cardNumber);
            _paymentMethods.Add(paymentMethod);
        }

        /// <summary>
        /// Removes a payment method from the user.
        /// </summary>
        /// <param name="paymentMethod">The payment method to remove.</param>
        public void RemovePaymentMethod(PaymentMethod paymentMethod) => _paymentMethods.Remove(paymentMethod);
    }
}
