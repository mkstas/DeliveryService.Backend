namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents a restaurant with its addresses, users and ingredients.
    /// </summary>
    /// <param name="Id">The restaurant identifier.</param>
    /// <param name="Name">The restaurant name.</param>
    /// <param name="ImageUrl">The restaurant image URL.</param>
    /// <param name="Addresses">The restaurant addresses.</param>
    /// <param name="Users">The users assigned to the restaurant.</param>
    /// <param name="Ingredients">The ingredients available in the restaurant.</param>
    public record RestaurantResponse(
        Guid Id,
        string Name,
        string? ImageUrl,
        IReadOnlyList<RestaurantAddressResponse> Addresses,
        IReadOnlyList<RestaurantUserResponse> Users,
        IReadOnlyList<IngredientResponse> Ingredients);
}
