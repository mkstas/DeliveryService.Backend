using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Dishes
{
    /// <summary>
    /// Represents the data required to add an ingredient to a dish.
    /// </summary>
    /// <param name="IngredientId">The identifier of the ingredient.</param>
    public record AddDishIngredientRequest(
        [Required(ErrorMessage = "Ingredient id is required.")]
        Guid IngredientId);
}
