using System.Text.Json.Serialization;
using Domain.Abstractions;

namespace Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid DishId { get; private init; }

        public int Quantity { get; private set; }

        public Dish Dish { get; private init; } = null!;

        [JsonConstructor]
        private CartItem(Guid dishId, int quantity)
        {
            DishId = dishId;
            Quantity = quantity;
        }

        public static CartItem Create(Guid dishId, int quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            return new(dishId, quantity);
        }

        public void ChangeQuantity(int newQuantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newQuantity);

            Quantity = newQuantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
