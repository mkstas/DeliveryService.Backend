using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ICategoryRespository"/>.
    /// </summary>
    internal class CategoryRespository(PersistenceDbContext context) : ICategoryRespository
    {
        /// <inheritdoc />
        public async Task CreateCategoryAsync(Category category)
        {
            context.Categories.Add(category);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Category>(category.Name);
            }
        }

        /// <inheritdoc />
        public async Task<List<Category>> GetCategoriesAsync(string? search = null)
        {
            var query = context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var pattern = search.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(pattern));
            }

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        /// <inheritdoc />
        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Category>(category.Name);
            }
        }

        /// <inheritdoc />
        public async Task RemoveCategoryAsync(Category category)
        {
            context.Categories.Remove(category);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
            {
                throw new DeleteConstraintException<Category>(category.Id);
            }
        }
    }
}
