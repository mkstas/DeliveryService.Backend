using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to update an ingredient.
    /// </summary>
    public record UpdateRestaurantIngredientRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
