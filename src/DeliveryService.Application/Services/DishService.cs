using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Application.Services
{
    public class DishService(
        IDishRepository dishRepository,
        IEstablishmentRepository establishmentRepository,
        IIngredientRepository ingredientRepository,
        IModifierRepository modifierRepository) : IDishService
    {
        public async Task CreateDishAsync(Guid establishmentId, Guid categoryId, StringBounded name, StringUnbounded? description = null)
        {
            _ = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId) ?? throw new NotFoundException<Establishment>(establishmentId);
            await dishRepository.CreateDishAsync(Dish.Create(establishmentId, categoryId, name, description));
        }

        public async Task<Dish> GetDishByIdAsync(Guid dishId)
        {
            return await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await dishRepository.GetDishesAsync();
        }

        public async Task UpdateDishAsync(Guid dishId, Guid categoryId, StringBounded newName, StringUnbounded? newDescription = null)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);

            dish.ChangeCategory(categoryId);
            dish.ChangeName(newName);
            dish.ChangeDescription(newDescription);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task AddIngredientAsync(Guid dishId, Guid ingredientId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
            var ingredient = await ingredientRepository.GetIngredientByIdAsync(ingredientId) ?? throw new NotFoundException<Ingredient>(ingredientId);

            dish.AddIngredient(ingredient);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task RemoveIngredientAsync(Guid dishId, Guid ingredientId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
            var ingredient = await ingredientRepository.GetIngredientByIdAsync(ingredientId) ?? throw new NotFoundException<Ingredient>(ingredientId);

            dish.RemoveIngredient(ingredient);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task AddModifierAsync(Guid dishId, Guid modifierId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
            var modifier = await modifierRepository.GetModifierByIdAsync(modifierId) ?? throw new NotFoundException<Modifier>(modifierId);

            dish.AddModifier(modifier);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task RemoveModifierAsync(Guid dishId, Guid modifierId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
            var modifier = await modifierRepository.GetModifierByIdAsync(modifierId) ?? throw new NotFoundException<Modifier>(modifierId);

            dish.RemoveModifier(modifier);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task CreateVariantAsync(Guid dishId, Currency price)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);

            dish.CreateVariant(price);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task UpdateVariantAsync(Guid dishId, Guid dishVariantId, Currency newPrice)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
            var variant = dish.Variants.FirstOrDefault(v => v.Id == dishVariantId) ?? throw new NotFoundException<DishVariant>(dishVariantId);

            variant.ChangePrice(newPrice);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task AddVariantSpecificationAsync(Guid dishId, Guid dishVariantId, Guid specificationId, StringBounded value)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
            var variant = dish.Variants.FirstOrDefault(v => v.Id == dishVariantId) ?? throw new NotFoundException<DishVariant>(dishVariantId);

            variant.CreateSpecification(specificationId, value);

            await dishRepository.UpdateDishAsync(dish);
        }

        public async Task RemoveVariantSpecificationAsync(Guid dishId, Guid dishVariantId, Guid dishSpecificationId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId) ?? throw new NotFoundException<Dish>(dishId);
            var variant = dish.Variants.FirstOrDefault(v => v.Id == dishVariantId) ?? throw new NotFoundException<DishVariant>(dishVariantId);

            variant.RemoveSpecification(dishSpecificationId);

            await dishRepository.UpdateDishAsync(dish);
        }
    }
}
