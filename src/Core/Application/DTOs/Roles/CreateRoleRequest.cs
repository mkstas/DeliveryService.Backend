using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Roles
{
    /// <summary>
    /// Represents the data required to create a new role.
    /// </summary>
    /// <param name="Name">The role name.</param>
    public record CreateRoleRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
