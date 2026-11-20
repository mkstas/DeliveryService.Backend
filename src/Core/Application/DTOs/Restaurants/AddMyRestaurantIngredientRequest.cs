using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Restaurants
{
    /// <summary>
    /// Represents the data required to add an ingredient to the restaurant of the current user.
    /// </summary>
    /// <param name="Name">The ingredient name.</param>
    public record AddMyRestaurantIngredientRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
