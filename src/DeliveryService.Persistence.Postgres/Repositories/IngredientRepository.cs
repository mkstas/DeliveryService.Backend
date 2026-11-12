using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class IngredientRepository(DeliveryServiceDbContext context) : IIngredientRepository
    {
        public async Task<Ingredient?> GetIngredientByIdAsync(Guid ingredientId)
        {
            return await context.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredientId);
        }

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await context.Ingredients
                .Include(i => i.Establishment)
                .ToListAsync();
        }

        public async Task<List<Ingredient>> GetIngredientsByEstablishmentIdAsync(Guid establishmentId)
        {
            return await context.Ingredients
                .Include(i => i.Establishment)
                .Where(i => i.EstablishmentId == establishmentId)
                .ToListAsync();
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await context.SaveChangesAsync();
        }
    }
}
