using DeliveryService.Web.Contracts.Permissions;

namespace DeliveryService.Web.Contracts.Roles
{
    /// <summary>
    /// Represents a role with its assigned permissions.
    /// </summary>
    /// <param name="Id">The role's identifier.</param>
    /// <param name="Name">The role's name.</param>
    /// <param name="Permissions">The permissions assigned to the role.</param>
    public record RoleResponse(
        Guid Id,
        string Name,
        List<PermissionResponse> Permissions);
}
