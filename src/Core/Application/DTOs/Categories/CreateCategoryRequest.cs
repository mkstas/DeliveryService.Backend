using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Categories
{
    /// <summary>
    /// Represents the data required to create a new category.
    /// </summary>
    /// <param name="Name">The category name.</param>
    public record CreateCategoryRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
