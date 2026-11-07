using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Specifications
{
    /// <summary>
    /// Represents the data required to create a new specification.
    /// </summary>
    /// <param name="Name">The specification name.</param>
    public record CreateSpecificationRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name);
}
