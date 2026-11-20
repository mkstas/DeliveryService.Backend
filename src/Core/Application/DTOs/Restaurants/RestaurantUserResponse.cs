namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents a user assigned to a restaurant.
    /// </summary>
    /// <param name="Id">The user identifier.</param>
    /// <param name="EmailAddress">The user email address.</param>
    /// <param name="Name">The user name.</param>
    /// <param name="ImageUrl">The URL of the user image.</param>
    public record RestaurantUserResponse(
        Guid Id,
        string EmailAddress,
        string Name,
        string? ImageUrl);
}
