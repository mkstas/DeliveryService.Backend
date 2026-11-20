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

        public async Task<List<Dish>> GetDishesAsync(string? search = null, Guid? restaurantId = null)
        {
            var query = context.Dishes
                .Include(d => d.Restaurant)
                .Include(d => d.Category)
                .Include(d => d.Specifications)
                .Include(d => d.Ingredients)
                    .ThenInclude(di => di.Ingredient)
                .AsQueryable();

            if (restaurantId is not null)
            {
                query = query.Where(d => d.RestaurantId == restaurantId);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var pattern = search.Trim().ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(pattern));
            }

            return await query.ToListAsync();
        }

        public async Task<Dish?> GetDishByIdAsync(Guid dishId)
        {
            return await context.Dishes
                .Include(d => d.Restaurant)
                .Include(d => d.Category)
                .Include(d => d.Specifications)
                .Include(d => d.Ingredients)
                    .ThenInclude(di => di.Ingredient)
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

        public async Task AddDishIngredientAsync(DishIngredient dishIngredient)
        {
            // Add explicitly so EF treats the entity as new (INSERT) instead of relying on
            // change tracking of the dish navigation, which produced an UPDATE.
            context.DishIngredients.Add(dishIngredient);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<DishIngredient>(dishIngredient.IngredientId.ToString());
            }
        }

        public async Task RemoveDishIngredientAsync(DishIngredient dishIngredient)
        {
            context.DishIngredients.Remove(dishIngredient);
            await context.SaveChangesAsync();
        }
    }
}
