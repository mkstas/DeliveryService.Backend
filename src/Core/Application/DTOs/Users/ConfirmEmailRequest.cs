using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents the data required to confirm a user email address.
    /// </summary>
    /// <param name="Code">The confirmation code sent to the email address.</param>
    /// <param name="EmailAddress">The email address to confirm.</param>
    public record ConfirmEmailRequest(
        [Required(ErrorMessage = "Code is required.")]
        string Code,

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        string EmailAddress);
}
