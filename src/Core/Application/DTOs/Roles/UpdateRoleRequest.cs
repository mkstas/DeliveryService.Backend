using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Roles
{
    /// <summary>
    /// Represents the data required to update a role.
    /// </summary>
    /// <param name="Name">The role name.</param>
    public record UpdateRoleRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
