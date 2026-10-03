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

        private readonly List<CartItem> _cartItems = [];
        /// <summary>
        /// The cart items of this cart.
        /// </summary>
        public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

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
        /// Adds a cart item to the cart for the specified menu item variant and ingredients.
        /// </summary>
        /// <param name="menuItemVariantId">The identifier of the menu item variant to add.</param>
        /// <param name="ingredients">The list of ingredients to include with the cart item.</param>
        /// <param name="quantity">The quantity of the menu item variant to add.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        public void CreateCartItem(Guid menuItemVariantId, List<Ingredient> ingredients, uint quantity)
        {

            var existingCartItem = FindCartItem(menuItemVariantId, ingredients);

            if (existingCartItem is not null)
            {
                existingCartItem.IncreaseQuantity(quantity);
            }
            else
            {
                _cartItems.Add(CartItem.Create(Id, menuItemVariantId, quantity));
            }
        }

        private CartItem? FindCartItem(Guid menuItemVariantId, List<Ingredient> ingredients)
            => _cartItems.Find(i => i.MenuItemVariantId == menuItemVariantId && i.HasSameIngredients(ingredients));

        /// <summary>
        /// Removes the cart item with the specified identifier from the cart.
        /// </summary>
        /// <param name="cartItemId">The identifier of the cart item to remove.</param>
        public void RemoveCartItem(Guid cartItemId)
        {
            var cartItem = _cartItems.Find(i => i.Id == cartItemId);

            if (cartItem is not null)
            {
                _cartItems.Remove(cartItem);
            }
        }

        /// <summary>
        /// Removes the specified cart item from the cart.
        /// </summary>
        /// <param name="cartItem">The cart item to remove.</param>
        public void RemoveCartItem(CartItem cartItem) => _cartItems.Remove(cartItem);

        /// <summary>
        /// Increases the quantity of the cart item for the given menu item variant and set of ingredients.
        /// </summary>
        /// <param name="menuItemVariantId">The identifier of the menu item variant.</param>
        /// <param name="ingredients">The ingredients to compare against.</param>
        /// <param name="quantity">The quantity to add.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown when the resulting quantity exceeds <see cref="uint.MaxValue"/>.
        /// </exception>
        public void IncreaseCartItemQuantity(Guid menuItemVariantId, List<Ingredient> ingredients, uint quantity)
            => FindCartItem(menuItemVariantId, ingredients)?.IncreaseQuantity(quantity);

        /// <summary>
        /// Decreases the quantity of the cart item for the given menu item variant and set of ingredients.
        /// </summary>
        /// <param name="menuItemVariantId">The identifier of the menu item variant.</param>
        /// <param name="ingredients">The ingredients to compare against.</param>
        /// <param name="quantity">The quantity to subtract.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        public void DecreaseCartItemQuantity(Guid menuItemVariantId, List<Ingredient> ingredients, uint quantity)
        {
            var cartItem = FindCartItem(menuItemVariantId, ingredients);

            if (cartItem is null)
            {
                return;
            }

            cartItem.DecreaseQuantity(quantity);

            if (cartItem.Quantity == 0)
            {
                _cartItems.Remove(cartItem);
            }
        }
    }
}
