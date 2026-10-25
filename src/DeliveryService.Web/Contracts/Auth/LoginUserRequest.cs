using DeliveryService.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Auth
{
    /// <summary>
    /// Represents the credentials required to log in a user.
    /// </summary>
    /// <param name="EmailAddress">The user's email address.</param>
    /// <param name="Password">The user's password.</param>
    public record LoginUserRequest(
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_LENGTH,
            ErrorMessage = "Email address cannot exceed 254 characters.")]
        string EmailAddress,

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(32, ErrorMessage = "Password cannot exceed 32 characters.")]
        string Password);
}
