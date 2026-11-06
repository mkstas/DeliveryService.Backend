using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Modifiers
{
    /// <summary>
    /// Represents the data required to updata an ingredient.
    /// </summary>
    /// <param name="Name">The ingredient's name.</param>
    public record UpdateModifiertRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name,

        [Required(ErrorMessage = "Price is required.")]
        decimal Price);
}
