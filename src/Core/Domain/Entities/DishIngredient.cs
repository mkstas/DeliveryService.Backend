using System.Text.Json.Serialization;
using Domain.Abstractions;

namespace Domain.Entities
{
    /// <summary>
    /// Represents an ingredient included in a dish.
    /// </summary>
    public class DishIngredient : BaseEntity
    {
        /// <summary>
        /// The identifier of the dish.
        /// </summary>
        public Guid DishId { get; private init; }

        /// <summary>
        /// The identifier of the ingredient.
        /// </summary>
        public Guid IngredientId { get; private init; }

        /// <summary>
        /// The dish the ingredient is included in.
        /// </summary>
        public Dish Dish { get; private init; } = null!;

        /// <summary>
        /// The ingredient included in the dish.
        /// </summary>
        public Ingredient Ingredient { get; private init; } = null!;

        [JsonConstructor]
        private DishIngredient(Guid dishId, Guid ingredientId)
        {
            DishId = dishId;
            IngredientId = ingredientId;
        }

        /// <summary>
        /// Creates a new <see cref="DishIngredient"/> instance.
        /// </summary>
        /// <param name="dishId">The identifier of the dish.</param>
        /// <param name="ingredientId">The identifier of the ingredient.</param>
        /// <returns>A new <see cref="DishIngredient"/> instance.</returns>
        public static DishIngredient Create(Guid dishId, Guid ingredientId)
        {
            return new(dishId, ingredientId);
        }
    }
}
