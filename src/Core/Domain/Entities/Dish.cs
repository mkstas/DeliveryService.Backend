using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a dish offered by a restaurant.
    /// </summary>
    public class Dish : BaseEntity
    {
        /// <summary>
        /// The identifier of the restaurant the dish belongs to.
        /// </summary>
        public Guid RestaurantId { get; private init; }

        /// <summary>
        /// The identifier of the category the dish belongs to.
        /// </summary>
        public Guid CategoryId { get; private init; }

        /// <summary>
        /// The name of the dish.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The price of the dish.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// The description of the dish, if any.
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// The image URL of the dish, if any.
        /// </summary>
        public string? ImageUrl { get; private set; }

        /// <summary>
        /// The restaurant the dish belongs to.
        /// </summary>
        public Restaurant Restaurant { get; private init; } = null!;

        /// <summary>
        /// The category the dish belongs to.
        /// </summary>
        public Category Category { get; private init; } = null!;

        private readonly List<DishSpecification> _specifications = [];
        /// <summary>
        /// The specifications associated with the dish.
        /// </summary>
        public IReadOnlyList<DishSpecification> Specifications => _specifications.AsReadOnly();

        private readonly List<DishIngredient> _ingredients = [];
        /// <summary>
        /// The ingredients included in the dish.
        /// </summary>
        public IReadOnlyList<DishIngredient> Ingredients => _ingredients.AsReadOnly();

        private readonly List<CartItem> _cartItems = [];
        /// <summary>
        /// The cart items containing the dish.
        /// </summary>
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

        /// <summary>
        /// Creates a new <see cref="Dish"/> instance.
        /// </summary>
        /// <param name="restaurantId">The identifier of the restaurant the dish belongs to.</param>
        /// <param name="categoryId">The identifier of the category the dish belongs to.</param>
        /// <param name="name">The name of the dish. Must not be null or empty.</param>
        /// <param name="price">The price of the dish. Must not be negative.</param>
        /// <param name="description">The description of the dish, if any.</param>
        /// <param name="imageUrl">The image URL of the dish, if any.</param>
        /// <returns>A new <see cref="Dish"/> instance.</returns>
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

        /// <summary>
        /// Changes the name of the dish.
        /// </summary>
        /// <param name="newName">The new name to assign to the dish. Must not be null or empty.</param>
        public void ChangeName(string newName)
        {
            ValidationHelper.CheckNotNullOrEmpty(newName, nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the price of the dish.
        /// </summary>
        /// <param name="newPrice">The new price to assign to the dish. Must not be negative.</param>
        public void ChangePrice(decimal newPrice)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(newPrice);

            Price = newPrice;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the description of the dish.
        /// </summary>
        /// <param name="newDescription">The new description, or <c>null</c> to remove it.</param>
        public void ChangeDescription(string? newDescription)
        {
            Description = newDescription;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the image URL of the dish.
        /// </summary>
        /// <param name="newImageUrl">The new image URL, or <c>null</c> to remove it.</param>
        public void ChangeImageUrl(string? newImageUrl)
        {
            ImageUrl = newImageUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds a specification to the dish.
        /// </summary>
        /// <param name="specification">The specification to add.</param>
        public void AddSpecification(DishSpecification specification)
        {
            _specifications.Add(specification);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes a specification from the dish.
        /// </summary>
        /// <param name="specification">The specification to remove.</param>
        public void RemoveSpecification(DishSpecification specification)
        {
            _specifications.Remove(specification);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds an ingredient to the dish.
        /// </summary>
        /// <param name="ingredient">The ingredient to add.</param>
        public void AddIngredient(DishIngredient ingredient)
        {
            _ingredients.Add(ingredient);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes an ingredient from the dish.
        /// </summary>
        /// <param name="ingredient">The ingredient to remove.</param>
        public void RemoveIngredient(DishIngredient ingredient)
        {
            _ingredients.Remove(ingredient);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds a cart item containing the dish.
        /// </summary>
        /// <param name="cartItem">The cart item to add.</param>
        public void AddCartItem(CartItem cartItem)
        {
            _cartItems.Add(cartItem);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes a cart item containing the dish.
        /// </summary>
        /// <param name="cartItem">The cart item to remove.</param>
        public void RemoveCartItem(CartItem cartItem)
        {
            _cartItems.Remove(cartItem);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
