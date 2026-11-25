using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to dishes.
    /// </summary>
    public interface IDishRepository
    {
        /// <summary>
        /// Creates a new dish.
        /// </summary>
        /// <param name="dish">The dish to create.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateDishAsync(Dish dish);

        /// <summary>
        /// Returns a dish by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>The matching dish, or <c>null</c> if none found.</returns>
        Task<Dish?> GetDishByIdAsync(Guid dishId);

        /// <summary>
        /// Returns dishes in the system, optionally filtered.
        /// </summary>
        /// <param name="search">Optional substring to match against the dish name.</param>
        /// <param name="restaurantId">Optional restaurant identifier to restrict the result.</param>
        /// <param name="categoryId">Optional category identifier to restrict the result.</param>
        /// <param name="minPrice">Optional inclusive minimum price.</param>
        /// <param name="maxPrice">Optional inclusive maximum price.</param>
        /// <param name="restaurantIds">Optional restaurant identifiers to restrict the result.</param>
        /// <returns>The matching dishes.</returns>
        Task<List<Dish>> GetDishesAsync(
            string? search = null,
            Guid? restaurantId = null,
            Guid? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            IReadOnlyCollection<Guid>? restaurantIds = null);

        /// <summary>
        /// Updates the specified dish.
        /// </summary>
        /// <param name="dish">The dish to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateDishAsync(Dish dish);

        /// <summary>
        /// Removes the specified dish.
        /// </summary>
        /// <param name="dish">The dish to remove.</param>
        /// <returns>A task that represents the asynchronous remove operation.</returns>
        Task RemoveDishAsync(Dish dish);

        /// <summary>
        /// Adds an ingredient to a dish.
        /// </summary>
        /// <param name="dishIngredient">The dish ingredient to add.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task AddDishIngredientAsync(DishIngredient dishIngredient);

        /// <summary>
        /// Removes an ingredient from a dish.
        /// </summary>
        /// <param name="dishIngredient">The dish ingredient to remove.</param>
        /// <returns>A task that represents the asynchronous remove operation.</returns>
        Task RemoveDishIngredientAsync(DishIngredient dishIngredient);
    }
}
