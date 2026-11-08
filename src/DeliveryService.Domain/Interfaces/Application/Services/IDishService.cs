using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides operations for managing dishes.
    /// </summary>
    public interface IDishService
    {
        /// <summary>
        /// Creates a new dish.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier of the dish to create.</param>
        /// <param name="categoryId">The category identifier of the dish to create.</param>
        /// <param name="name">The name of the dish to create.</param>
        /// <param name="description">The description of the dish to create.</param>
        Task CreateDishAsync(Guid establishmentId, Guid categoryId, StringBounded name, StringUnbounded? description = null);

        /// <summary>
        /// Returns a dish by its identifier.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>The matching dish.</returns>
        Task<Dish> GetDishByIdAsync(Guid dishId);

        /// <summary>
        /// Returns all dishes in the system.
        /// </summary>
        /// <returns>All dishes in the system.</returns>
        Task<List<Dish>> GetDishesAsync();

        /// <summary>
        /// Updates the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="categoryId">The new category id of the dish.</param>
        /// <param name="newName">The new name id of the dish.</param>
        /// <param name="newDescription">The new description id of the dish.</param>
        Task UpdateDishAsync(Guid dishId, Guid categoryId, StringBounded newName, StringUnbounded? newDescription = null);

        /// <summary>
        /// Assigns an ingredient to the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        Task AddIngredientAsync(Guid dishId, Guid ingredientId);

        /// <summary>
        /// Removes an ingredient from the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        Task RemoveIngredientAsync(Guid dishId, Guid ingredientId);

        /// <summary>
        /// Assigns a modifier to the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="modifierId">The modifier identifier.</param>
        Task AddModifierAsync(Guid dishId, Guid modifierId);

        /// <summary>
        /// Removes a modifier from the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="modifierId">The modifier identifier.</param>
        Task RemoveModifierAsync(Guid dishId, Guid modifierId);

        /// <summary>
        /// Creates a dish variant.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="price">The price of the dish variant to create.</param>
        Task CreateVariantAsync(Guid dishId, Currency price);

        /// <summary>
        /// Updates the specified dish variant.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="dishVariantId">The dish variant identifier.</param>
        /// <param name="newPrice">The new price of the dish variant.</param>
        Task UpdateVariantAsync(Guid dishId, Guid dishVariantId, Currency newPrice);

        /// <summary>
        /// Adds a specification to the specified dish variant.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="dishVariantId">The dish variant identifier.</param>
        /// <param name="specificationId">The specification identifier.</param>
        /// <param name="value">The value of the specification to add.</param>
        Task AddVariantSpecificationAsync(Guid dishId, Guid dishVariantId, Guid specificationId, StringBounded value);

        /// <summary>
        /// Removes a dish specification from the specified dish variant.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="dishVariantId">The dish variant identifier.</param>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        Task RemoveVariantSpecificationAsync(Guid dishId, Guid dishVariantId, Guid dishSpecificationId);
    }
}
