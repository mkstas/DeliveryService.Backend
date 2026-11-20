using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to update a restaurant address.
    /// </summary>
    /// <param name="Address">The new address text.</param>
    public record UpdateRestaurantAddressRequest(
        [Required(ErrorMessage = "Address is required.")]
        string Address);
}
