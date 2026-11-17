using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to create a new restaurant.
    /// </summary>
    /// <param name="Name">The restaurant name.</param>
    public record CreateRestaurantRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
