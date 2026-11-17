using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Categories
{
    /// <summary>
    /// Represents the data required to update an existing category.
    /// </summary>
    /// <param name="Name">The category new name.</param>
    public record UpdateCategoryRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
