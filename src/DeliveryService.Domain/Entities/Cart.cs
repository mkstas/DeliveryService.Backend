using DeliveryService.Domain.Common.Abstracts;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a cart within the system.
    /// </summary>
    public class Cart : Entity
    {
        /// <summary>
        /// The identifier of the user who owns this cart.
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// The user who owns this cart.
        /// </summary>
        public User User { get; private set; } = null!;

        private readonly List<CartItem> _items = [];
        /// <summary>
        /// The cart items of this cart.
        /// </summary>
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

        private Cart(Guid userId)
        {
            Id = Guid.Empty;
            UserId = userId;
        }

        /// <summary>
        /// Creates a new <see cref="Cart"/> instance.
        /// </summary>
        /// <param name="userId">The identifier of the user who owns the cart.</param>
        /// <returns>A new <see cref="Cart"/> instance.</returns>
        internal static Cart Create(Guid userId) => new(userId);

        /// <summary>
        /// Increases the quantity of a cart item for the specified dish variant and modifiers.
        /// </summary>
        /// <param name="dishVariantId">The identifier of the dish variant.</param>
        /// <param name="modifiers">The list of modifiers to include with the cart item.</param>
        /// <param name="quantity">The quantity of the dish variant.</param>
        public void IncreaseCartItemQuantity(Guid dishVariantId, List<Modifier> modifiers, uint quantity)
        {
            var existingCartItem = FindCartItem(dishVariantId, modifiers);

            if (existingCartItem is not null)
            {
                existingCartItem.IncreaseQuantity(quantity);
            }
            else
            {
                var cartItem = CartItem.Create(Id, dishVariantId, quantity, modifiers);
                _items.Add(cartItem);
            }
        }

        /// <summary>
        /// Decreases the quantity of a cart item for the specified dish variant and modifiers.
        /// </summary>
        /// <param name="dishVariantId">The identifier of the dish variant.</param>
        /// <param name="modifiers">The list of modifiers to include with the cart item.</param>
        /// <param name="quantity">The quantity of the dish variant to subtract.</param>
        public void DecreaseCartItemQuantity(Guid dishVariantId, List<Modifier> modifiers, uint quantity)
        {
            var existingCartItem = FindCartItem(dishVariantId, modifiers);

            if (existingCartItem is null) return;

            existingCartItem.DecreaseQuantity(quantity);

            if (existingCartItem.Quantity == 0)
            {
                _items.Remove(existingCartItem);
            }
        }

        private CartItem? FindCartItem(Guid dishVariantId, List<Modifier> modifiers)
            => _items.Find(ci => ci.DishVariantId == dishVariantId && ci.HasSameModifiers(modifiers));
    }
}
