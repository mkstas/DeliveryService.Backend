using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Specifications
{
    /// <summary>
    /// Represents the data required to update an existing specification.
    /// </summary>
    /// <param name="Name">The specification new name.</param>
    public record UpdateSpecificationRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name);
}
