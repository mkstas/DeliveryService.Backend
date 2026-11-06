using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Ingredients
{
    /// <summary>
    /// Represents the data required to updata an ingredient.
    /// </summary>
    /// <param name="Name">The ingredient's name.</param>
    public record UpdateIngredientRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name);
}
