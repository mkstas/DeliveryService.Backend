using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to update a restaurant.
    /// </summary>
    /// <param name="Name">The restaurant name.</param>
    public record UpdateRestaurantRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
