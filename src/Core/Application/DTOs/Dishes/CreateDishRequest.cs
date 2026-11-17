using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Dishes
{
    public record CreateDishRequest(
        Guid RestaurantId,
        Guid CategoryId,

        [Required(ErrorMessage = "Name is required.")]
        string Name,

        decimal Price,
        string? Description = null,
        string? ImageUrl = null);
}
