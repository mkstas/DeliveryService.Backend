using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Auth
{
    /// <summary>
    /// Represents the data required to confirm a user's email address.
    /// </summary>
    /// <param name="EmailAddress">The email address to confirm.</param>
    /// <param name="Code">The confirmation code sent to the email address.</param>
    public record ConfirmEmailRequest(
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_LENGTH,
            ErrorMessage = "Email address cannot exceed 254 characters.")]
        string EmailAddress,

        [Required(ErrorMessage = "Code is required.")]
        string Code);
}
