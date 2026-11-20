using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class DishSpecificationRepository(PersistenceDbContext context) : IDishSpecificationRepository
    {
        public async Task CreateDishSpecificationAsync(DishSpecification dishSpecification)
        {
            context.DishSpecifications.Add(dishSpecification);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<DishSpecification>(dishSpecification.Value);
            }
        }

        public async Task<List<DishSpecification>> GetDishSpecificationsAsync()
        {
            return await context.DishSpecifications
                .Include(ds => ds.Dish)
                .Include(ds => ds.Specification)
                .ToListAsync();
        }

        public async Task<DishSpecification?> GetDishSpecificationByIdAsync(Guid dishSpecificationId)
        {
            return await context.DishSpecifications
                .Include(ds => ds.Dish)
                .Include(ds => ds.Specification)
                .FirstOrDefaultAsync(ds => ds.Id == dishSpecificationId);
        }

        public async Task UpdateDishSpecificationAsync(DishSpecification dishSpecification)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<DishSpecification>(dishSpecification.Value);
            }
        }

        public async Task RemoveDishSpecificationAsync(DishSpecification dishSpecification)
        {
            context.DishSpecifications.Remove(dishSpecification);
            await context.SaveChangesAsync();
        }
    }
}
