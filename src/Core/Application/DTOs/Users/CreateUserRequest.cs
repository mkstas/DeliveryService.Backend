using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents the data required to create a new user.
    /// </summary>
    /// <param name="EmailAddress">The user email address.</param>
    /// <param name="Password">The user password.</param>
    /// <param name="Name">The user name.</param>
    public record CreateUserRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name,

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        string EmailAddress,

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        string Password);
}
