using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Roles
{
    /// <summary>
    /// Represents the data required to assign a permission to a role.
    /// </summary>
    /// <param name="RoleId">The role identifier.</param>
    /// <param name="PermissionId">The permission identifier.</param>
    public record AddRolePermissionsRequest(
        [Required(ErrorMessage = "Role id is required.")]
        Guid RoleId,

        [Required(ErrorMessage = "Permission id is required.")]
        Guid PermissionId);
}
