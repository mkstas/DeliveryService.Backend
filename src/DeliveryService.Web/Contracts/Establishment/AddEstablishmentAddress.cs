using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Establishment
{
    public record AddEstablishmentAddressRequest(
        [Required(ErrorMessage = "Establishment id is required.")]
        Guid EstablishmentId,

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Address cannot exceed 256 characters.")]
        string Address);
}
