using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    public record UpdateRestaurantAddressRequest(
        [Required(ErrorMessage = "Address is required.")]
        string Address);
}
