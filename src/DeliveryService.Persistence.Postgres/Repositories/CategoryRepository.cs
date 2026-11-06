using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Persistence.Postgres.Common;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class CategoryRepository(DeliveryServiceDbContext context) : ICategoryRepository
    {
        public async Task CreateCategoryAsync(Category category)
        {
            context.Categories.Add(category);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Category>(category.Name);
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await context.Categories
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await context.Categories
                .Include(c => c.Dishes)
                .ToListAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Category>(category.Name);
            }
        }
    }
}
