using Domain.Entities.Common;
using Domain.Helpers;

namespace Domain.Entities.Catalog
{
    /// <summary>
    /// Represents a named permission grant within the system.
    /// </summary>
    public class Permission : BaseEntity
    {
        /// <summary>
        /// The internal name of the permission.
        /// </summary>
        public string SystemName { get; private init; }

        /// <summary>
        /// The display name of the permission.
        /// </summary>
        public string DisplayName { get; private init; }

        private readonly List<Role> _roles = [];
        /// <summary>
        /// The roles that grant this permission.
        /// </summary>
        public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

        private Permission(string systemName, string displayName)
        {
            SystemName = systemName;
            DisplayName = displayName;
        }

        /// <summary>
        /// Creates a new <see cref="Permission"/> instance.
        /// </summary>
        /// <param name="systemName">Must not be null or empty.</param>
        /// <param name="displayName">Must not be null or empty.</param>
        /// <returns>A new <see cref="Permission"/> instance.</returns>
        public static Permission Create(string systemName, string displayName)
        {
            ValidationHelper.CheckNotNullOrEmpty(systemName, nameof(systemName));
            ValidationHelper.CheckNotNullOrEmpty(displayName, nameof(displayName));

            return new(systemName, displayName);
        }
    }
}
