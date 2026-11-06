using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Application.Services
{
    public class ModifierService(
        IEstablishmentRepository establishmentRepository,
        IModifierRepository modifierRepository) : IModifierService
    {
        public async Task CreateModifierAsync(Guid establishmentId, StringBounded name, Currency price)
        {
            var establishment = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);

            establishment.CreateModifier(name, price);
        }

        public async Task UpdateModifierAsync(Guid modifierId, StringBounded newName, Currency newPrice)
        {
            var modifier = await modifierRepository.GetModifierByIdAsync(modifierId)
                ?? throw new NotFoundException<Ingredient>(modifierId);

            modifier.ChangeName(newName);
            modifier.ChangePrice(newPrice);
        }
    }
}
