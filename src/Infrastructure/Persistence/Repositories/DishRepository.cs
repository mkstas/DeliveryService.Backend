using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class DishRepository(PersistenceDbContext context) : IDishRepository
    {
        public async Task CreateDishAsync(Dish dish)
        {
            context.Dishes.Add(dish);
            await context.SaveChangesAsync();
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await context.Dishes
                .Include(d => d.Restaurant)
                .Include(d => d.Category)
                .Include(d => d.Specifications)
                .ToListAsync();
        }

        public async Task<Dish?> GetDishByIdAsync(Guid dishId)
        {
            return await context.Dishes
                .Include(d => d.Restaurant)
                .Include(d => d.Category)
                .Include(d => d.Specifications)
                .FirstOrDefaultAsync(d => d.Id == dishId);
        }

        public async Task UpdateDishAsync(Dish dish)
        {
            await context.SaveChangesAsync();
        }

        public async Task RemoveDishAsync(Dish dish)
        {
            context.Dishes.Remove(dish);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
            {
                throw new DeleteConstraintException<Dish>(dish.Id);
            }
        }
    }
}
