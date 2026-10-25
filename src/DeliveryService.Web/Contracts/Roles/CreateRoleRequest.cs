using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Roles
{
    /// <summary>
    /// Represents the data required to create a new role.
    /// </summary>
    /// <param name="Name">The role's name.</param>
    public record CreateRoleRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name);
}
