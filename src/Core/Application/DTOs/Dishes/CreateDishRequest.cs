using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Dishes
{
    /// <summary>
    /// Represents the data required to create a new dish.
    /// </summary>
    /// <param name="RestaurantId">The restaurant identifier.</param>
    /// <param name="CategoryId">The category identifier.</param>
    /// <param name="Name">The dish name.</param>
    /// <param name="Price">The dish price.</param>
    /// <param name="Description">The dish description, if any.</param>
    /// <param name="ImageUrl">The dish image URL, if any.</param>
    public record CreateDishRequest(
        Guid RestaurantId,
        Guid CategoryId,

        [Required(ErrorMessage = "Name is required.")]
        string Name,

        decimal Price,
        string? Description = null,
        string? ImageUrl = null);
}
