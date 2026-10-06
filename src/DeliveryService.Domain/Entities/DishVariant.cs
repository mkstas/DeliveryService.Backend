using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a dish variant within the system.
    /// </summary>
    public class DishVariant : Entity
    {
        /// <summary>
        /// The identifier of the dish this variant belongs to.
        /// </summary>
        public Guid DishId { get; init; }

        /// <summary>
        /// The price of the variant.
        /// </summary>
        public Currency Price { get; private set; }

        /// <summary>
        /// The image URL of the variant, if any.
        /// </summary>
        public StringBounded? ImageUrl { get; private set; }

        /// <summary>
        /// The dish this variant belongs to.
        /// </summary>
        public Dish Dish { get; init; } = null!;

        private readonly List<DishSpecification> _specifications = [];
        /// <summary>
        /// The specifications of the variant.
        /// </summary>
        public IReadOnlyList<DishSpecification> Specifications => _specifications.AsReadOnly();

        private DishVariant(Guid dishId, Currency price, StringBounded? imageUrl = null)
        {
            Id = Guid.Empty;
            DishId = dishId;
            Price = price;
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// Creates a new <see cref="DishVariant"/> instance.
        /// </summary>
        /// <param name="dishId">The identifier of the dish this variant belongs to.</param>
        /// <param name="price">Must not be negative.</param>
        /// <param name="imageUrl">If provided, must not be empty or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="DishVariant"/> instance.</returns>
        internal static DishVariant Create(Guid dishId, Currency price, StringBounded? imageUrl = null) => new(dishId, price, imageUrl);

        /// <summary>
        /// Changes the price of the variant.
        /// </summary>
        /// <param name="newPrice">The new price to assign to the variant.</param>
        public void ChangePrice(Currency newPrice) => Price = newPrice;

        /// <summary>
        /// Changes the image URL of the variant.
        /// </summary>
        /// <param name="newImageUrl">The new image URL to assign to the variant.</param>
        public void ChangeImageUrl(StringBounded? newImageUrl = null) => ImageUrl = newImageUrl;

        /// <summary>
        /// Creates a specification for the variant.
        /// </summary>
        /// <param name="specificationId">The identifier of the specification to associate.</param>
        /// <param name="value">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        public void CreateSpecification(Guid specificationId, StringBounded value)
        {
            var specification = DishSpecification.Create(Id, specificationId, value);
            _specifications.Add(specification);
        }

        /// <summary>
        /// Removes a specification from the variant.
        /// </summary>
        /// <param name="specification">The specification to remove.</param>
        public void RemoveSpecification(DishSpecification specification) => _specifications.Remove(specification);
    }
}
