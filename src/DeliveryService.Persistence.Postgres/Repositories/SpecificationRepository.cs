using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Persistence.Postgres.Common;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class SpecificationRepository(DeliveryServiceDbContext context) : ISpecificationRepository
    {
        public async Task CreateSpecificationAsync(Specification specification)
        {
            context.Specifications.Add(specification);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Category>(specification.Name);
            }
        }

        public async Task<Specification?> GetSpecificationByIdAsync(Guid specificationId)
        {
            return await context.Specifications
                .FirstOrDefaultAsync(s => s.Id == specificationId);
        }

        public async Task<List<Specification>> GetSpecificationsAsync()
        {
            return await context.Specifications.ToListAsync();
        }

        public async Task UpdateCategoryAsync(Specification specification)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Category>(specification.Name);
            }
        }
    }
}
