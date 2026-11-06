using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Category
{
    /// <summary>
    /// Represents the data required to create a new category.
    /// </summary>
    /// <param name="Name">The category's name.</param>
    public record CreateCategoryRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name);
}
