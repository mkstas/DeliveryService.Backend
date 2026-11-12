using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides operations for managing ingredients.
    /// </summary>
    public interface IIngredientService
    {
        /// <summary>
        /// Creates a new ingredient.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier.</param>
        /// <param name="name">The name of the ingredient to create.</param>
        Task CreateIngredientAsync(Guid establishmentId, StringBounded name);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
         Task<List<Ingredient>> GetIngredientsAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <returns></returns>
         Task<List<Ingredient>> GetIngredientsByEstablishmentIdAsync(Guid establishmentId);

        /// <summary>
        /// Updates the name of the specified ingredient.
        /// </summary>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <param name="newName">The new name of the ingredient.</param>
        Task UpdateIngredientAsync(Guid ingredientId, StringBounded newName);
    }
}
