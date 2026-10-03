using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents the association between an order item and an ingredient.
    /// </summary>
    public class OrderItemIngredient : Entity
    {
        /// <summary>
        /// The identifier of the order item this association belongs to.
        /// </summary>
        public Guid OrderItemId { get; init; }

        /// <summary>
        /// The identifier of the ingredient this association refers to.
        /// </summary>
        public Guid IngredientId { get; init; }

        /// <summary>
        /// The price of the ingredient.
        /// </summary>
        public Currency Price { get; init; }

        /// <summary>
        /// Indicates whether the ingredient is additional.
        /// </summary>
        public bool IsAdditional { get; init; }

        /// <summary>
        /// The order item this association belongs to.
        /// </summary>
        public OrderItem OrderItem { get; init; } = null!;

        /// <summary>
        /// The ingredient this association refers to.
        /// </summary>
        public Ingredient Ingredient { get; init; } = null!;

        private OrderItemIngredient(Guid orderItemId, Guid ingredientId, Currency price, bool isAdditional)
        {
            Id = Guid.Empty;
            OrderItemId = orderItemId;
            IngredientId = ingredientId;
            Price = price;
            IsAdditional = isAdditional;
        }

        /// <summary>
        /// Creates a new <see cref="OrderItemIngredient"/> instance.
        /// </summary>
        /// <param name="orderItemId">The identifier of the order item this association belongs to.</param>
        /// <param name="ingredientId">The identifier of the ingredient this association refers to.</param>
        /// <param name="price">The price of the ingredient.</param>
        /// <param name="isAdditional">Indicates whether the ingredient is additional.</param>
        /// <returns>A new <see cref="OrderItemIngredient"/> instance.</returns>
        internal static OrderItemIngredient Create(Guid orderItemId, Guid ingredientId, Currency price, bool isAdditional)
            => new(orderItemId, ingredientId, price, isAdditional);
    }
}
