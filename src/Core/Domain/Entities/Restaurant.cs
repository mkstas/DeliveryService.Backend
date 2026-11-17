using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a restaurant within the system.
    /// </summary>
    public class Restaurant : BaseEntity
    {
        /// <summary>
        /// The name of the restaurant.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The image URL of the restaurant, if any.
        /// </summary>
        public string? ImageUrl { get; private set; } = null;

        private readonly List<User> _users = [];
        /// <summary>
        /// The users associated with the restaurant.
        /// </summary>
        public IReadOnlyList<User> Users => _users.AsReadOnly();

        private readonly List<RestaurantAddress> _addresses = [];
        /// <summary>
        /// The addresses of the restaurant.
        /// </summary>
        public IReadOnlyList<RestaurantAddress> Addresses => _addresses.AsReadOnly();

        private readonly List<Ingredient> _ingredients = [];
        /// <summary>
        /// The ingredients available to the restaurant.
        /// </summary>
        public IReadOnlyList<Ingredient> Ingredients => _ingredients.AsReadOnly();

        [JsonConstructor]
        private Restaurant(string name, string? imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// Creates a new <see cref="Restaurant"/> instance.
        /// </summary>
        /// <param name="name">Must not be null or empty.</param>
        /// <param name="imageUrl">The image URL, if any.</param>
        /// <returns>A new <see cref="Restaurant"/> instance.</returns>
        public static Restaurant Create(string name, string? imageUrl = null)
        {
            ValidationHelper.CheckNotNullOrEmpty(name, nameof(name));

            return new(name, imageUrl);
        }

        /// <summary>
        /// Changes the name of the restaurant.
        /// </summary>
        /// <param name="newName">The new name to assign to the restaurant.</param>
        public void ChangeName(string newName)
        {
            ValidationHelper.CheckNotNullOrEmpty(newName, nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the image URL of the restaurant.
        /// </summary>
        /// <param name="newImageUrl">The new image URL, or <c>null</c> to remove it.</param>
        public void ChangeImageUrl(string? newImageUrl)
        {
            ImageUrl = newImageUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds a user to the restaurant.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(User user)
        {
            _users.Add(user);
            user.AssignToRestaurant(Id);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes a user from the restaurant.
        /// </summary>
        /// <param name="user">The user to remove.</param>
        public void RemoveUser(User user)
        {
            _users.Remove(user);
            user.RemoveFromRestaurant();
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds an address to the restaurant.
        /// </summary>
        /// <param name="address">Must not be null or empty.</param>
        public void AddAddress(string address)
        {
            var restaurantAddress = RestaurantAddress.Create(Id, address);
            _addresses.Add(restaurantAddress);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes an address from the restaurant.
        /// </summary>
        /// <param name="address">The address to remove.</param>
        public void RemoveAddress(RestaurantAddress address)
        {
            _addresses.Remove(address);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds an ingredient for the establishment.
        /// </summary>
        /// <param name="name">Must not be null or empty.</param>
        public void AddIngredient(string name)
        {
            var ingredient = Ingredient.Create(Id, name);
            _ingredients.Add(ingredient);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes an ingredient from the establishment.
        /// </summary>
        /// <param name="ingredient">The ingredient to remove.</param>
        public void RemoveIngredient(Ingredient ingredient)
        {
            _ingredients.Remove(ingredient);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
