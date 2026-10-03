using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents the value of a specification assigned to a menu item variant.
    /// </summary>
    public class MenuItemSpecification : Entity
    {
        /// <summary>
        /// The identifier of the menu item variant this specification belongs to.
        /// </summary>
        public Guid MenuItemVariantId { get; init; }

        /// <summary>
        /// The identifier of the specification this value refers to.
        /// </summary>
        public Guid SpecificationId { get; init; }

        /// <summary>
        /// The value of the specification.
        /// </summary>
        public StringBounded Value { get; init; }

        /// <summary>
        /// Gets the menu item variant this specification value belongs to.
        /// </summary>
        public MenuItemVariant MenuItemVariant { get; init; } = null!;

        /// <summary>
        /// Gets the specification this value refers to.
        /// </summary>
        public Specification Specification { get; init; } = null!;

        private MenuItemSpecification(Guid menuItemVariantId, Guid specificationId, StringBounded value)
        {
            Id = Guid.Empty;
            MenuItemVariantId = menuItemVariantId;
            SpecificationId = specificationId;
            Value = value;
        }

        /// <summary>
        /// Creates a new <see cref="MenuItemSpecification"/> instance.
        /// </summary>
        /// <param name="menuItemVariantId">The identifier of the menu item variant this specification value belongs to.</param>
        /// <param name="specificationId">The identifier of the specification this value refers to.</param>
        /// <param name="value">The value of the specification. Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="MenuItemSpecification"/> instance.</returns>
        internal static MenuItemSpecification Create(Guid menuItemVariantId, Guid specificationId, StringBounded value)
            => new(menuItemVariantId, specificationId, value);
    }
}
