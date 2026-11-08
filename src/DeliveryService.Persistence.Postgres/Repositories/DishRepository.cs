using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Persistence.Postgres.Common;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class DishRepository(DeliveryServiceDbContext context) : IDishRepository
    {
        public async Task CreateDishAsync(Dish dish)
        {
            context.Dishes.Add(dish);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Dish>(dish.Name);
            }
        }

        public async Task<Dish?> GetDishByIdAsync(Guid dishId)
        {
            return await context.Dishes
                .Include(d => d.Ingredients)
                .Include(d => d.Modifiers)
                .Include(d => d.Variants)
                .ThenInclude(v => v.Specifications)
                .ThenInclude(s => s.Specification)
                .FirstOrDefaultAsync(d => d.Id == dishId);
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await context.Dishes
                .Include(d => d.Ingredients)
                .Include(d => d.Modifiers)
                .Include(d => d.Variants)
                .ThenInclude(v => v.Specifications)
                .ThenInclude(s => s.Specification)
                .ToListAsync();
        }

        public async Task UpdateDishAsync(Dish dish)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Dish>(dish.Name);
            }
        }
    }
}
