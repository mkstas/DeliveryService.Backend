using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Persistence.Postgres.Common;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class EstablishmentRepository(DeliveryServiceDbContext context) : IEstablishmentRepository
    {
        public async Task CreateEstablishmentAsync(Establishment establishment)
        {
            context.Establishments.Add(establishment);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Establishment>(establishment.BusinessAccount);
            }
        }

        public async Task<Establishment?> GetEstablishmentByIdAsync(Guid establishmentId)
        {
            return await context.Establishments
                .Include(e => e.Addresses)
                .FirstOrDefaultAsync(e => e.Id == establishmentId);
        }

        public async Task<List<Establishment>> GetEstablishmentsAsync()
        {
            return await context.Establishments
                .Include(e => e.Addresses)
                .ToListAsync();
        }

        public async Task UpdateEstablishmentAsync(Establishment establishment)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Establishment>(establishment.BusinessAccount);
            }
        }
    }
}
