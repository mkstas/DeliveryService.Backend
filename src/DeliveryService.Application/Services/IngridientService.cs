using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Application.Services
{
    public class IngredientService(
        IEstablishmentRepository establishmentRepository,
        IIngredientRepository ingredientRepository) : IIngredientService
    {
        public async Task CreateIngredientAsync(Guid establishmentId, StringBounded name)
        {
            var establishment = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);

            establishment.CreateIngredient(name);

            await establishmentRepository.UpdateEstablishmentAsync(establishment);
        }

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await ingredientRepository.GetIngredientsAsync();
        }

        public async Task<List<Ingredient>> GetIngredientsByEstablishmentIdAsync(Guid establishmentId)
        {
            return await ingredientRepository.GetIngredientsByEstablishmentIdAsync(establishmentId);
        }

        public async Task UpdateIngredientAsync(Guid ingredientId, StringBounded newName)
        {
            var ingredient = await ingredientRepository.GetIngredientByIdAsync(ingredientId)
                ?? throw new NotFoundException<Ingredient>(ingredientId);

            ingredient.ChangeName(newName);

            await ingredientRepository.UpdateIngredientAsync(ingredient);
        }
    }
}
