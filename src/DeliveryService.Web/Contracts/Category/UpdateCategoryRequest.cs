using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Category
{
    /// <summary>
    /// Represents the data required to update an existing category.
    /// </summary>
    /// <param name="Name">The category's new name.</param>
    public record UpdateCategoryRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name);
}
