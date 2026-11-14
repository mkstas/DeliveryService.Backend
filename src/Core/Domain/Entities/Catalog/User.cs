using Domain.Entities.Common;
using Domain.Helpers;

namespace Domain.Entities.Catalog
{
    /// <summary>
    /// Represents a user within the system.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string EmailAddress { get; private set; }

        /// <summary>
        /// The password hash of the user.
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The image URL of the user, if any.
        /// </summary>
        public string? ImageUrl { get; private set; } = null;

        private readonly List<Role> _roles = [];
        /// <summary>
        /// The roles assigned to the user.
        /// </summary>
        public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

        private User(string emailAddress, string passwordHash, string name)
        {
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
            Name = name;
        }

        /// <summary>
        /// Creates a new <see cref="User"/> instance.
        /// </summary>
        /// <param name="emailAddress">>Must be correct email address format.</param>
        /// <param name="passwordHash">Must not be null or empty.</param>
        /// <param name="name">Must not be null or empty.</param>
        /// <returns>A new <see cref="User"/> instance.</returns>
        public static User Create(string emailAddress, string passwordHash, string name)
        {
            ValidationHelper.CheckCorrectEmailFormat(emailAddress, nameof(emailAddress));
            ValidationHelper.CheckNotNullOrEmpty(passwordHash, nameof(passwordHash));
            ValidationHelper.CheckNotNullOrEmpty(name, nameof(name));

            return new(emailAddress, passwordHash, name);
        }

        /// <summary>
        /// Changes the email address of the user.
        /// </summary>
        /// <param name="newEmailAddress">The new email address to assign to the user.</param>
        public void ChangeEmailAddress(string newEmailAddress)
        {
            ValidationHelper.CheckCorrectEmailFormat(newEmailAddress, nameof(newEmailAddress));

            EmailAddress = newEmailAddress;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="newName">The new name to assign to the user.</param>
        public void ChangeName(string newName)
        {
            ValidationHelper.CheckNotNullOrEmpty(newName, nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the image URL of the user.
        /// </summary>
        /// <param name="newImageUrl">The new image URL to assign to the user.</param>
        public void ChangeImageUrl(string? newImageUrl = null)
        {
            ImageUrl = newImageUrl;
            UpdatedAt = DateTime.UtcNow;
        }

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
    }
}
