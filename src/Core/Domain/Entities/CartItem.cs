using System.Text.Json.Serialization;
using Domain.Abstractions;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a dish added to a user's cart.
    /// </summary>
    public class CartItem : BaseEntity
    {
        /// <summary>
        /// The identifier of the user the cart item belongs to.
        /// </summary>
        public Guid UserId { get; private init; }

        /// <summary>
        /// The identifier of the dish in the cart.
        /// </summary>
        public Guid DishId { get; private init; }

        /// <summary>
        /// The number of dish portions in the cart.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// The user the cart item belongs to.
        /// </summary>
        public User User { get; private init; } = null!;

        /// <summary>
        /// The dish in the cart.
        /// </summary>
        public Dish Dish { get; private init; } = null!;

        [JsonConstructor]
        private CartItem(Guid userId, Guid dishId, int quantity)
        {
            UserId = userId;
            DishId = dishId;
            Quantity = quantity;
        }

        /// <summary>
        /// Creates a new <see cref="CartItem"/> instance.
        /// </summary>
        /// <param name="userId">The identifier of the user the cart item belongs to.</param>
        /// <param name="dishId">The identifier of the dish in the cart.</param>
        /// <param name="quantity">The number of dish portions. Must be greater than zero.</param>
        /// <returns>A new <see cref="CartItem"/> instance.</returns>
        public static CartItem Create(Guid userId, Guid dishId, int quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            return new(userId, dishId, quantity);
        }

        /// <summary>
        /// Changes the number of dish portions in the cart.
        /// </summary>
        /// <param name="newQuantity">The new quantity. Must be greater than zero.</param>
        public void ChangeQuantity(int newQuantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newQuantity);

            Quantity = newQuantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
