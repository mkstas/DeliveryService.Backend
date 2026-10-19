using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    /// Represents the data required to update a user's email address.
    /// </summary>
    /// <param name="EmailAddress">The user's new email address.</param>
    public record UpdateUserEmailRequest(
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_LENGTH,
            ErrorMessage = "Email address cannot exceed 254 characters.")]
        string EmailAddress);
}
