using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Modifiers
{
    /// <summary>
    /// Represents the data required to update an existing modifier.
    /// </summary>
    /// <param name="Name">The modifier's new name.</param>
    /// <param name="Price">The modifier's new price.</param>
    public record UpdateModifierRequest (
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name,

        [Required(ErrorMessage = "Price is required.")]
        decimal Price);
}
