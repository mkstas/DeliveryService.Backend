using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to assign a user to a restaurant.
    /// </summary>
    /// <param name="RestaurantId">The restaurant identifier.</param>
    /// <param name="EmailAddress">The user email address.</param>
    public record AddRestaurantUserRequest(
        [Required(ErrorMessage = "Restaurant id is required.")]
        Guid RestaurantId,

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        string EmailAddress);
}
