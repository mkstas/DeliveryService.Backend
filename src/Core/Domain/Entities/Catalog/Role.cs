using Domain.Entities.Common;
using Domain.Helpers;

namespace Domain.Entities.Catalog
{
    /// <summary>
    /// Represents a named role within the system.
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// The name of the role.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Indicates whether the role is built-in.
        /// </summary>
        public bool IsBuiltIn { get; private init; }

        private readonly List<Permission> _permissions = [];
        /// <summary>
        /// The permissions granted by this role.
        /// </summary>
        public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();

        private readonly List<User> _users = [];
        /// <summary>
        /// The users assigned to this role.
        /// </summary>
        public IReadOnlyList<User> Users => _users.AsReadOnly();

        private Role(string name, bool isBuiltIn = false)
        {
            Name = name;
            IsBuiltIn = isBuiltIn;
        }

        /// <summary>
        /// Creates a new <see cref="Role"/> instance.
        /// </summary>
        /// <param name="name">Must not be null or empty.</param>
        /// <param name="isBuiltIn">Must be <c>true</c> if the role is built-in.</param>
        /// <returns>A new <see cref="Role"/> instance.</returns>
        public static Role Create(string name, bool isBuiltIn = false)
        {
            ValidationHelper.CheckNotNullOrEmpty(name, nameof(name));

            return new(name, isBuiltIn);
        }

        /// <summary>
        /// Changes the name of the role.
        /// </summary>
        /// <param name="newName">Must not be null or empty.</param>
        /// <exception cref="InvalidOperationException">Thrown when attempting to rename a built-in role.</exception>
        public void ChangeName(string newName)
        {
            if (IsBuiltIn)
                throw new InvalidOperationException($"Built-in '{Name}' role cannot be renamed.");

            ValidationHelper.CheckNotNullOrEmpty(newName, nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

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
