using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents the data required to update a user bio.
    /// </summary>
    /// <param name="Name">The user new name.</param>
    public record UpdateUserRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
