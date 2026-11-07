using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Establishments
{
    /// <summary>
    /// Represents the data required to associate a user with an establishment.
    /// </summary>
    /// <param name="EstablishmentId">The establishment identifier.</param>
    /// <param name="EmailAddress">The email address of the user to add.</param>
    public record AddUserEstablishmentRequest(
        [Required(ErrorMessage = "User id is required.")]
        Guid EstablishmentId,

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_LENGTH,
            ErrorMessage = "Email address cannot exceed 254 characters.")]
        string EmailAddress);
}
