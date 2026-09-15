using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a named permission grant within the system.
    /// </summary>
    public class Permission : Entity
    {
        /// <summary>
        /// The internal name of the permission.
        /// </summary>
        public StringBounded Name { get; init; }

        private readonly List<Role> _roles = [];
        /// <summary>
        /// The roles that grant this permission.
        /// </summary>
        public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

        /// <summary>
        /// The display name of the permission.
        /// </summary>
        public StringBounded DisplayName { get; init; }

        private Permission(StringBounded name, StringBounded displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        /// <summary>
        /// Creates a new <see cref="Permission"/> instance.
        /// </summary>
        /// <param name="name">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="displayName">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="Permission"/> instance.</returns>
        public static Permission Create(StringBounded name, StringBounded displayName) => new(name, displayName);
    }
}
