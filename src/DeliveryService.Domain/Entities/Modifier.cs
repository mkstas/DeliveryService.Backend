using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a modifier within the system.
    /// </summary>
    public class Modifier : Entity
    {
        /// <summary>
        /// The identifier of the establishment this modifier belongs to.
        /// </summary>
        public Guid EstablishmentId { get; init; }

        /// <summary>
        /// The name of the modifier.
        /// </summary>
        public StringBounded Name { get; private set; }

        /// <summary>
        /// The price of the modifier.
        /// </summary>
        public Currency Price { get; private set; }

        /// <summary>
        /// The image URL of the modifier, if any.
        /// </summary>
        public StringBounded? ImageUrl { get; private set; }

        private readonly List<Dish> _dishes = [];
        /// <summary>
        /// The dishes associated with this modifier.
        /// </summary>
        public IReadOnlyList<Dish> Dishes => _dishes.AsReadOnly();

        private Modifier(Guid establishmentId, StringBounded name, Currency price, StringBounded? imageUrl = null)
        {
            Id = Guid.Empty;
            EstablishmentId = establishmentId;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// Creates a new <see cref="Modifier"/> instance.
        /// </summary>
        /// <param name="establishmentId">The identifier of the establishment this modifier belongs to.</param>
        /// <param name="name">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="price">Must not be negative.</param>
        /// <param name="imageUrl">If provided, must not be empty or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="Modifier"/> instance.</returns>
        internal static Modifier Create(Guid establishmentId, StringBounded name, Currency price, StringBounded? imageUrl = null)
            => new(establishmentId, name, price, imageUrl);
    }
}
