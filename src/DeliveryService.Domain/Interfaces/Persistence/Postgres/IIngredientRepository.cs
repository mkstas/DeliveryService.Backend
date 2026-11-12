using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Persistence.Postgres
{
    /// <summary>
    /// Provides read/write access to ingredients.
    /// </summary>
    public interface IIngredientRepository
    {
        /// <summary>
        /// Returns an ingredient by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <returns>The matching ingredient, or <c>null</c> if none found.</returns>
        Task<Ingredient?> GetIngredientByIdAsync(Guid ingredientId);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<List<Ingredient>> GetIngredientsAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <returns></returns>
        Task<List<Ingredient>> GetIngredientsByEstablishmentIdAsync(Guid establishmentId);

        /// <summary>
        /// Updates the specified ingredient.
        /// </summary>
        /// <param name="ingredient">The ingredient to update.</param>
        Task UpdateIngredientAsync(Ingredient ingredient);
    }
}
