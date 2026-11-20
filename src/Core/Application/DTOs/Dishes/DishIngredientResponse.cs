namespace Application.DTOs.Dishes
{
    /// <summary>
    /// Represents an ingredient included in a dish.
    /// </summary>
    /// <param name="Id">The dish ingredient identifier.</param>
    /// <param name="IngredientId">The identifier of the ingredient.</param>
    /// <param name="Name">The name of the ingredient.</param>
    public record DishIngredientResponse(
        Guid Id,
        Guid IngredientId,
        string Name);
}
