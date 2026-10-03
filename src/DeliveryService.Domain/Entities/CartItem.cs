using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.Common.Helpers;

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
        /// The identifier of the menu item variant this cart item refers to.
        /// </summary>
        public Guid MenuItemVariantId { get; init; }

        /// <summary>
        /// The quantity of the menu item variant in the cart.
        /// </summary>
        public uint Quantity { get; private set; }

        /// <summary>
        /// The cart this cart item belongs to.
        /// </summary>
        public Cart Cart { get; init; } = null!;

        /// <summary>
        /// The menu item variant this cart item refers to.
        /// </summary>
        public MenuItemVariant MenuItemVariant { get; init; } = null!;

        private readonly List<Ingredient> _ingredients = [];
        /// <summary>
        /// The ingredients associated with this cart item.
        /// </summary>
        public IReadOnlyList<Ingredient> Ingredients => _ingredients.AsReadOnly();

        private CartItem(Guid cartId, Guid menuItemVariantId, uint quantity)
        {
            CartId = cartId;
            MenuItemVariantId = menuItemVariantId;
            Quantity = quantity;
        }

        /// <summary>
        /// Creates a new <see cref="CartItem"/> instance.
        /// </summary>
        /// <param name="cartId">The identifier of the cart this cart item belongs to.</param>
        /// <param name="menuItemVariantId">The identifier of the menu item variant this cart item refers to.</param>
        /// <param name="quantity">The quantity of the menu item variant in the cart.</param>
        /// <returns>A new <see cref="CartItem"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        internal static CartItem Create(Guid cartId, Guid menuItemVariantId, uint quantity)
        {
            ValidationHelper.CheckGreaterThanZero(quantity, nameof(quantity));

            return new(cartId, menuItemVariantId, quantity);
        }

        /// <summary>
        /// Determines whether this cart item contains exactly the same ingredients as the specified list.
        /// </summary>
        /// <param name="ingredients">The list of ingredients to compare against.</param>
        /// <returns><see langword="true"/> if both ingredient sets are equal; otherwise <see langword="false"/>.</returns>
        public bool HasSameIngredients(List<Ingredient> ingredients) => Ingredients.Select(i => i.Id).SequenceEqual(ingredients.Select(i => i.Id));

        /// <summary>
        /// Changes the quantity of the cart item.
        /// </summary>
        /// <param name="quantity">The new quantity to assign to the cart item.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        public void ChangeQuantity(uint quantity)
        {
            ValidationHelper.CheckGreaterThanZero(quantity, nameof(quantity));
            Quantity = quantity;
        }

        /// <summary>
        /// Increases the quantity of the cart item.
        /// </summary>
        /// <param name="quantity">The quantity to add to the cart item.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown when the resulting quantity exceeds <see cref="uint.MaxValue"/>.
        /// </exception>
        public void IncreaseQuantity(uint quantity)
        {
            ValidationHelper.CheckGreaterThanZero(quantity, nameof(quantity));
            Quantity = checked(Quantity + quantity);
        }

        /// <summary>
        /// Decreases the quantity of the cart item, clamping it to zero instead of underflowing.
        /// </summary>
        /// <param name="quantity">The quantity to subtract from the cart item.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        public void DecreaseQuantity(uint quantity)
        {
            ValidationHelper.CheckGreaterThanZero(quantity, nameof(quantity));
            Quantity = quantity >= Quantity ? 0 : Quantity - quantity;
        }
    }
}
