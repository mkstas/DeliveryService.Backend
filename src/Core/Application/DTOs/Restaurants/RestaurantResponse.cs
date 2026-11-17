namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents a restaurant with its addresses and users.
    /// </summary>
    /// <param name="Id">The restaurant identifier.</param>
    /// <param name="Name">The restaurant name.</param>
    /// <param name="ImageUrl">The restaurant image URL.</param>
    /// <param name="Addresses">The restaurant addresses.</param>
    /// <param name="Users">The users assigned to the restaurant.</param>
    public record RestaurantResponse(
        Guid Id,
        string Name,
        string? ImageUrl,
        IReadOnlyList<RestaurantAddressResponse> Addresses,
        IReadOnlyList<RestaurantUserResponse> Users);
}
