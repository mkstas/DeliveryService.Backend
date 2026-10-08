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

        private readonly List<Establishment> _establishments = [];
        /// <summary>
        /// The establishments associated with the user.
        /// </summary>
        public IReadOnlyList<Establishment> Establishments => _establishments.AsReadOnly();

        /// <summary>
        /// The cart of the user, if one exists.
        /// </summary>
        public Cart? Cart { get; private set; }

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

        private User(EmailAddress emailAddress, StringUnbounded passwordHash)
        {
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
        }

        /// <summary>
        /// Creates a new <see cref="User"/> instance.
        /// </summary>
        /// <param name="emailAddress">Must not be null, empty, or exceed <see cref="EmailAddress.MAX_LENGTH"/> characters.</param>
        /// <param name="passwordHash">Must not be null or empty.</param>
        /// <returns>A new <see cref="User"/> instance.</returns>
        public static User Create(EmailAddress emailAddress, StringUnbounded passwordHash) => new(emailAddress, passwordHash);

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
        /// Creates a payment method for the user using the specified card number.
        /// </summary>
        /// <param name="cardNumber">Must not be null or empty, and must consist of exactly <see cref="CardNumber.LENGTH"/> digits.</param>
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

        /// <summary>
        /// Creates a cart for the user if one does not already exist.
        /// </summary>
        public void CreateCart() => Cart ??= Cart.Create(Id);
    }
}
