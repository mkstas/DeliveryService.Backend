using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Auth
{
    /// <summary>
    /// Represents the data required to resend an email address confirmation code.
    /// </summary>
    /// <param name="EmailAddress">The email address to resend the confirmation code to.</param>
    public record ResendConfirmationRequest(
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_LENGTH,
            ErrorMessage = "Email address cannot exceed 254 characters.")]
        string EmailAddress);
}
