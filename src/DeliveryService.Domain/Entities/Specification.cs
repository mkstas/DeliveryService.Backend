using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a specification within the system.
    /// </summary>
    public class Specification : Entity
    {
        /// <summary>
        /// The name of the specification.
        /// </summary>
        public StringBounded Name { get; private set; }

        private readonly List<MenuItemSpecification> _menuItemSpecifications = [];
        /// <summary>
        /// The menu item specifications that reference this specification.
        /// </summary>
        public IReadOnlyList<MenuItemSpecification> MenuItemSpecifications => _menuItemSpecifications.AsReadOnly();

        private Specification(StringBounded name) => Name = name;

        /// <summary>
        /// Creates a new <see cref="Specification"/> instance.
        /// </summary>
        /// <param name="name">The name of the specification. Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="Specification"/> instance.</returns>
        public static Specification Create(StringBounded name) => new(name);

        /// <summary>
        /// Changes the name of the specification.
        /// </summary>
        /// <param name="newName">The new name to assign to the specification.</param>
        public void ChangeName(StringBounded newName) => Name = newName;
    }
}
