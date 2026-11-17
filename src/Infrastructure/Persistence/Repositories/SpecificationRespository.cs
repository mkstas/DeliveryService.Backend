using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ISpecificationRespository"/>.
    /// </summary>
    internal class SpecificationRespository(PersistenceDbContext context) : ISpecificationRespository
    {
        /// <inheritdoc />
        public async Task CreateSpecificationAsync(Specification specification)
        {
            context.Specifications.Add(specification);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Specification>(specification.Name);
            }
        }

        /// <inheritdoc />
        public async Task<List<Specification>> GetSpecificationsAsync()
        {
            return await context.Specifications.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Specification?> GetSpecificationByIdAsync(Guid specificationId)
        {
            return await context.Specifications.FirstOrDefaultAsync(s => s.Id == specificationId);
        }

        /// <inheritdoc />
        public async Task UpdateSpecificationAsync(Specification specification)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Specification>(specification.Name);
            }
        }

        /// <inheritdoc />
        public async Task RemoveSpecificationAsync(Specification specification)
        {
            context.Specifications.Remove(specification);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
            {
                throw new DeleteConstraintException<Specification>(specification.Id);
            }
        }
    }
}
