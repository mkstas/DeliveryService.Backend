using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to add an address to a restaurant.
    /// </summary>
    /// <param name="RestaurantId">The restaurant identifier.</param>
    /// <param name="Address">The address to add.</param>
    public record AddRestaurantAddressRequest(
        [Required(ErrorMessage = "Restaurant id is required.")]
        Guid RestaurantId,

        [Required(ErrorMessage = "Address is required.")]
        string Address);
}
