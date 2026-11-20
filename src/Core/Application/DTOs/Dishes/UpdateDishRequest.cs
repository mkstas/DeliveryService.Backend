using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Dishes
{
    /// <summary>
    /// Represents the data required to update an existing dish.
    /// </summary>
    /// <param name="Name">The dish name.</param>
    /// <param name="Price">The dish price.</param>
    /// <param name="Description">The dish description, if any.</param>
    /// <param name="ImageUrl">The dish image URL, if any.</param>
    public record UpdateDishRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name,

        decimal Price,
        string? Description = null,
        string? ImageUrl = null);
}
