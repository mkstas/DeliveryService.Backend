using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Dishes
{
    public record UpdateDishRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name,

        decimal Price,
        string? Description = null,
        string? ImageUrl = null);
}
