using Application.DTOs.Permissions;

namespace Application.DTOs.Roles
{
    /// <summary>
    /// Represents a role with its assigned permissions.
    /// </summary>
    /// <param name="Id">The role identifier.</param>
    /// <param name="Name">The role name.</param>
    /// <param name="Permissions">The permissions assigned to the role.</param>
    public record RoleResponse(
        Guid Id,
        string Name,
        IReadOnlyList<PermissionResponse> Permissions);
}
