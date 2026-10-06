using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a named role within the system.
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// The name of the role.
        /// </summary>
        public StringBounded Name { get; private set; }

        private readonly List<User> _users = [];
        /// <summary>
        /// The users assigned to this role.
        /// </summary>
        public IReadOnlyList<User> Users => _users.AsReadOnly();

        private readonly List<Permission> _permissions = [];
        /// <summary>
        /// The permissions granted by this role.
        /// </summary>
        public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();

        private Role(StringBounded name) => Name = name;

        /// <summary>
        /// Creates a new <see cref="Role"/> instance.
        /// </summary>
        /// <param name="name">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="Role"/> instance.</returns>
        public static Role Create(StringBounded name) => new(name);

        /// <summary>
        /// Changes the name of the role.
        /// </summary>
        /// <param name="newName">The new name to assign to the role.</param>
        public void ChangeName(StringBounded newName) => Name = newName;

        /// <summary>
        /// Adds a permission to the role.
        /// </summary>
        /// <param name="permission">The permission to add.</param>
        public void AddPermission(Permission permission) => _permissions.Add(permission);

        /// <summary>
        /// Removes a permission from the role.
        /// </summary>
        /// <param name="permission">The permission to remove.</param>
        public void RemovePermission(Permission permission) => _permissions.Remove(permission);
    }
}
