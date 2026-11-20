using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents an ingredient within the system.
    /// </summary>
    public class Ingredient : BaseEntity
    {
        /// <summary>
        /// The identifier of the restaurant this ingredient belongs to.
        /// </summary>
        public Guid RestaurantId { get; private init; }

        /// <summary>
        /// The name of the ingredient.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The restaurant this ingredient belongs to.
        /// </summary>
        public Restaurant Restaurant { get; private init; } = null!;

        [JsonConstructor]
        private Ingredient(Guid restaurantId, string name)
        {
            Id = Guid.Empty;
            RestaurantId = restaurantId;
            Name = name;
        }

        /// <summary>
        /// Creates a new <see cref="Ingredient"/> instance.
        /// </summary>
        /// <param name="restaurantId">The identifier of the restaurant this ingredient belongs to.</param>
        /// <param name="name">Must not be null or empty.</param>
        /// <returns>A new <see cref="Ingredient"/> instance.</returns>
        internal static Ingredient Create(Guid restaurantId, string name)
        {
            ValidationHelper.CheckNotNullOrEmpty(name, nameof(name));

            return new(restaurantId, name);
        }

        /// <summary>
        /// Changes the name of the ingredient.
        /// </summary>
        /// <param name="newName">The new name to assign to the ingredient.</param>
        public void ChangeName(string newName)
        {
            ValidationHelper.CheckNotNullOrEmpty(newName, nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
