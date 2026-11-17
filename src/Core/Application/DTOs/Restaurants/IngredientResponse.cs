namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents an ingredient.
    /// </summary>
    /// <param name="Id">The ingredient identifier.</param>
    /// <param name="RestaurantId">The restaurant identifier.</param>
    /// <param name="Name">The ingredient name.</param>
    public record IngredientResponse(
        Guid Id,
        Guid RestaurantId,
        string Name);
}
