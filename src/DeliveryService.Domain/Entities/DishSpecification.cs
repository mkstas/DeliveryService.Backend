using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents the value of a specification assigned to a dish variant.
    /// </summary>
    public class DishSpecification : Entity
    {
        /// <summary>
        /// The identifier of the dish variant this specification belongs to.
        /// </summary>
        public Guid DishVariantId { get; init; }

        /// <summary>
        /// The identifier of the specification this value refers to.
        /// </summary>
        public Guid SpecificationId { get; init; }

        /// <summary>
        /// The value of the specification.
        /// </summary>
        public StringBounded Value { get; init; }

        /// <summary>
        /// The dish variant this specification value belongs to.
        /// </summary>
        public DishVariant DishVariant { get; init; } = null!;

        /// <summary>
        /// The specification this value refers to.
        /// </summary>
        public Specification Specification { get; init; } = null!;

        private DishSpecification(Guid dishVariantId, Guid specificationId, StringBounded value)
        {
            Id = Guid.Empty;
            DishVariantId = dishVariantId;
            SpecificationId = specificationId;
            Value = value;
        }

        /// <summary>
        /// Creates a new <see cref="DishSpecification"/> instance.
        /// </summary>
        /// <param name="dishVariantId">The identifier of the dish variant this specification value belongs to.</param>
        /// <param name="specificationId">The identifier of the specification this value refers to.</param>
        /// <param name="value">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="DishSpecification"/> instance.</returns>
        internal static DishSpecification Create(Guid dishVariantId, Guid specificationId, StringBounded value)
            => new(dishVariantId, specificationId, value);
    }
}
