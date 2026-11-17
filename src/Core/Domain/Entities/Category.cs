using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a category within the system.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; private set; }

        [JsonConstructor]
        private Category(string name) => Name = name;

        /// <summary>
        /// Creates a new <see cref="Category"/> instance.
        /// </summary>
        /// <param name="name">Must not be null or empty.</param>
        /// <returns>A new <see cref="Category"/> instance.</returns>
        public static Category Create(string name)
        {
            ValidationHelper.CheckNotNullOrEmpty(name, nameof(name));

            return new(name);
        }

        /// <summary>
        /// Changes the name of the category.
        /// </summary>
        /// <param name="newName">The new name to assign to the category.</param>
        public void ChangeName(string newName)
        {
            ValidationHelper.CheckNotNullOrEmpty(newName, nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
