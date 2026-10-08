using DeliveryService.Domain.Common.Abstracts;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a cart item within the system.
    /// </summary>
    public class CartItem : Entity
    {
        /// <summary>
        /// The identifier of the cart this cart item belongs to.
        /// </summary>
        public Guid CartId { get; init; }

        /// <summary>
        /// The identifier of the dish variant this cart item refers to.
        /// </summary>
        public Guid DishVariantId { get; init; }

        /// <summary>
        /// The quantity of the dish variant in the cart.
        /// </summary>
        public uint Quantity { get; private set; }

        /// <summary>
        /// The cart this cart item belongs to.
        /// </summary>
        public Cart Cart { get; init; } = null!;

        /// <summary>
        /// The dish variant this cart item refers to.
        /// </summary>
        public DishVariant DishVariant { get; init; } = null!;

        private readonly List<Modifier> _modifiers = [];
        /// <summary>
        /// The modifiers associated with this cart item.
        /// </summary>
        public IReadOnlyList<Modifier> Modifiers => _modifiers.AsReadOnly();

        private CartItem() {}
        private CartItem(Guid cartId, Guid dishVariantId, uint quantity, List<Modifier> modifiers)
        {
            Id = Guid.Empty;
            CartId = cartId;
            DishVariantId = dishVariantId;
            Quantity = quantity;
            _modifiers.AddRange(modifiers);
        }

        /// <summary>
        /// Creates a new <see cref="CartItem"/> instance.
        /// </summary>
        /// <param name="cartId">The identifier of the cart this cart item belongs to.</param>
        /// <param name="dishVariantId">The identifier of the dish variant this cart item refers to.</param>
        /// <param name="quantity">The quantity of the dish variant in the cart.</param>
        /// <param name="modifiers">The modifiers to assign to the cart item.</param>
        /// <returns>A new <see cref="CartItem"/> instance.</returns>
        internal static CartItem Create(Guid cartId, Guid dishVariantId, uint quantity, List<Modifier> modifiers)
            => new(cartId, dishVariantId, quantity, modifiers);

        /// <summary>
        /// Determines whether this cart item has the same set of modifiers as the given list.
        /// </summary>
        /// <param name="modifiers">The modifiers to compare against.</param>
        /// <returns><c>true</c> if both contain the same modifier ids; otherwise <c>false</c>.</returns>
        public bool HasSameModifiers(List<Modifier> modifiers)
            => _modifiers.Select(m => m.Id).Order().SequenceEqual(modifiers.Select(m => m.Id).Order());

        /// <summary>
        /// Increases the quantity of the cart item.
        /// </summary>
        /// <param name="quantity">The quantity to add to the cart item.</param>
        public void IncreaseQuantity(uint quantity) =>  Quantity += quantity;

        /// <summary>
        /// Decreases the quantity of the cart item, clamping it to zero instead of underflowing.
        /// </summary>
        /// <param name="quantity">The quantity to subtract from the cart item.</param>
        public void DecreaseQuantity(uint quantity) => Quantity = quantity >= Quantity ? 0 : Quantity - quantity;
    }
}
