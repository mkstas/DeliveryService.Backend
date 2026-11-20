using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents the data required to resend an email address confirmation code.
    /// </summary>
    /// <param name="EmailAddress">The email address to resend the confirmation code to.</param>
    public record ResendConfirmEmailRequest(
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        string EmailAddress);
}
