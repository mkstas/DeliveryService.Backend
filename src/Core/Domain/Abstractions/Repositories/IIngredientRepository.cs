using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read access to restaurant ingredients.
    /// </summary>
    public interface IIngredientRepository
    {
        /// <summary>
        /// Returns an ingredient by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <returns>The matching ingredient, or <c>null</c> if none found.</returns>
        Task<Ingredient?> GetByIdAsync(Guid ingredientId);
    }
}
