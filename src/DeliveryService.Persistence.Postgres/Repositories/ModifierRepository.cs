using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class ModifierRepository(DeliveryServiceDbContext context) : IModifierRepository
    {
        public async Task<Modifier?> GetModifierByIdAsync(Guid modifierId)
        {
            return await context.Modifiers.FirstOrDefaultAsync(m => m.Id == modifierId);
        }

        public async Task UpdateModifierAsync(Modifier modifier)
        {
            await context.SaveChangesAsync();
        }
    }
}
