using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents the data required to log in a user.
    /// </summary>
    /// <param name="EmailAddress">The user email address.</param>
    /// <param name="Password">The user password.</param>
    public record LoginUserRequest(
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        string EmailAddress,

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        string Password);
}
