using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Modifiers
{
    /// <summary>
    /// Represents the data required to create a new ingredient.
    /// </summary>
    /// <param name="EstablishmentId">The establishment identifier.</param>
    /// <param name="Name">The ingredient's name.</param>
    public record CreateModifierRequest(
        [Required(ErrorMessage = "Establishment id is required.")]
        Guid EstablishmentId,

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name,

        [Required(ErrorMessage = "Price is required.")]
        decimal Price);
}
