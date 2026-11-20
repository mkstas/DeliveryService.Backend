using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IIngredientRepository"/>.
    /// </summary>
    internal class IngredientRepository(PersistenceDbContext context) : IIngredientRepository
    {
        /// <inheritdoc />
        public async Task<Ingredient?> GetByIdAsync(Guid ingredientId)
        {
            return await context.Ingredients
                .FirstOrDefaultAsync(i => i.Id == ingredientId);
        }
    }
}
