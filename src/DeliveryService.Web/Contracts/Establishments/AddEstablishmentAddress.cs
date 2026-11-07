using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Establishments
{
    /// <summary>
    /// Represents the data required to add an address to an establishment.
    /// </summary>
    /// <param name="EstablishmentId">The establishment identifier.</param>
    /// <param name="Address">The establishment address to add.</param>
    public record AddEstablishmentAddressRequest(
        [Required(ErrorMessage = "Establishment id is required.")]
        Guid EstablishmentId,

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Address cannot exceed 256 characters.")]
        string Address);
}
