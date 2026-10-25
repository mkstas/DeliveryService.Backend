using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Roles
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="RoleId"></param>
    /// <param name="PermissionId"></param>
    public record AddRolePermissionsRequest(
        [Required(ErrorMessage = "Role id is required.")]
        Guid RoleId,

        [Required(ErrorMessage = "Permission id is required.")]
        Guid PermissionId);
}
