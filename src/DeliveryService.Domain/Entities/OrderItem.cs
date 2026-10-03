using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.Common.Helpers;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a line item of an order within the system.
    /// </summary>
    public class OrderItem : Entity
    {
        /// <summary>
        /// The identifier of the order this order item belongs to.
        /// </summary>
        public Guid OrderId { get; init; }

        /// <summary>
        /// The identifier of the menu item variant this order item refers to.
        /// </summary>
        public Guid MenuItemVariantId { get; init; }

        /// <summary>
        /// The price of a single unit of the menu item variant at the moment the order was placed.
        /// </summary>
        public Currency Price { get; init; }

        /// <summary>
        /// The quantity of the menu item variant.
        /// </summary>
        public uint Quantity { get; init; }

        /// <summary>
        /// The order this order item belongs to.
        /// </summary>
        public Order Order { get; private set; } = null!;

        /// <summary>
        /// The menu item variant this order item refers to.
        /// </summary>
        public MenuItemVariant MenuItemVariant { get; private set; } = null!;

        private readonly List<OrderItemIngredient> _ingredients = [];
        /// <summary>
        /// The ingredients associated with this order item.
        /// </summary>
        public IReadOnlyList<OrderItemIngredient> Ingredients => _ingredients.AsReadOnly();

        private OrderItem(Guid orderId, Guid menuItemVariantId, Currency price, uint quantity, List<MenuItemIngredient> ingredients)
        {
            Id = Guid.Empty;
            OrderId = orderId;
            MenuItemVariantId = menuItemVariantId;
            Price = price;
            Quantity = quantity;
            CreateOrderItemIngredients(ingredients);
        }

        /// <summary>
        /// Creates a new <see cref="OrderItem"/> instance.
        /// </summary>
        /// <param name="orderId">The identifier of the order this order item belongs to.</param>
        /// <param name="menuItemVariantId">The identifier of the menu item variant this order item refers to.</param>
        /// <param name="price">The price of a single unit of the menu item variant.</param>
        /// <param name="quantity">The quantity of the menu item variant.</param>
        /// <returns>A new <see cref="OrderItem"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        internal static OrderItem Create(Guid orderId, Guid menuItemVariantId, Currency price, uint quantity, List<MenuItemIngredient> ingredients)
        {
            ValidationHelper.CheckGreaterThanZero(quantity, nameof(quantity));

            return new(orderId, menuItemVariantId, price, quantity, ingredients);
        }

        private void CreateOrderItemIngredients(List<MenuItemIngredient> ingredients)
        {
            foreach (var item in ingredients)
            {
                var ingredient = OrderItemIngredient.Create(Id, item.Ingredient.Id, item.Price, item.IsAdditional);
                _ingredients.Add(ingredient);
            }
        }
    }
}
