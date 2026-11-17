namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents a restaurant address.
    /// </summary>
    /// <param name="Id">The address identifier.</param>
    /// <param name="Address">The address text.</param>
    public record RestaurantAddressResponse(
        Guid Id,
        string Address);
}
