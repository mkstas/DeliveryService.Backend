using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    public class Dish : BaseEntity
    {
        public Guid RestaurantId { get; private init; }

        public Guid CategoryId { get; private init; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string? Description { get; private set; }

        public string? ImageUrl { get; private set; }

        public Restaurant Restaurant { get; private init; } = null!;

        public Category Category { get; private init; } = null!;

        private readonly List<DishSpecification> _specifications = [];
        public IReadOnlyList<DishSpecification> Specifications => _specifications.AsReadOnly();

        private readonly List<CartItem> _cartItems = [];
        public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

        [JsonConstructor]
        private Dish(Guid restaurantId, Guid categoryId, string name, decimal price, string? description, string? imageUrl)
        {
            RestaurantId = restaurantId;
            CategoryId = categoryId;
            Name = name;
            Price = price;
            Description = description;
            ImageUrl = imageUrl;
        }

        public static Dish Create(
            Guid restaurantId,
            Guid categoryId,
            string name,
            decimal price,
            string? description = null,
            string? imageUrl = null)
        {
            ValidationHelper.CheckNotNullOrEmpty(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegative(price);

            return new(restaurantId, categoryId, name, price, description, imageUrl);
        }

        public void ChangeName(string newName)
        {
            ValidationHelper.CheckNotNullOrEmpty(newName, nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePrice(decimal newPrice)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(newPrice);

            Price = newPrice;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeDescription(string? newDescription)
        {
            Description = newDescription;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeImageUrl(string? newImageUrl)
        {
            ImageUrl = newImageUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddSpecification(DishSpecification specification)
        {
            _specifications.Add(specification);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveSpecification(DishSpecification specification)
        {
            _specifications.Remove(specification);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddCartItem(CartItem cartItem)
        {
            _cartItems.Add(cartItem);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveCartItem(CartItem cartItem)
        {
            _cartItems.Remove(cartItem);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
