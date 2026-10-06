using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a category within the system.
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// Gets the name of the category.
        /// </summary>
        public StringBounded Name { get; private set; }

        private readonly List<Dish> _dishes = [];
        /// <summary>
        /// The dishes associated with this category.
        /// </summary>
        public IReadOnlyList<Dish> Dishes => _dishes.AsReadOnly();

        private Category(StringBounded name) => Name = name;

        /// <summary>
        /// Creates a new <see cref="Category"/> instance.
        /// </summary>
        /// <param name="name">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="Category"/> instance.</returns>
        public static Category Create(StringBounded name) => new(name);

        /// <summary>
        /// Changes the name of the category.
        /// </summary>
        /// <param name="newName">The new name to assign to the category.</param>
        public void ChangeName(StringBounded newName) => Name = newName;
    }
}
