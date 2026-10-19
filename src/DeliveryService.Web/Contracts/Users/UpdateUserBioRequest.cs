using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    /// Represents the data required to update a user's bio.
    /// </summary>
    /// <param name="Name">The user's new name.</param>
    public record UpdateUserBioRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name);
}
