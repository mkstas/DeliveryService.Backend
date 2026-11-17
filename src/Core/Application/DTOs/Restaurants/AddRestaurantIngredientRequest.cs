using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to add an ingredient to a restaurant.
    /// </summary>
    /// <param name="RestaurantId">The restaurant identifier.</param>
    public record AddRestaurantIngredientRequest(
        [Required(ErrorMessage = "Restaurant id is required.")]
        Guid RestaurantId,

        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
