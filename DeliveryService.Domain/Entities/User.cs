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
        /// <param name="firstName">Must not be null, empty, or exceed <see cref="EmailAddress.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="User"/> instance.</returns>
        public static User Create(EmailAddress emailAddress, StringUnbounded passwordHash, StringBounded firstName)
            => new(emailAddress, passwordHash, firstName);
    }
}
