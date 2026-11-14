namespace Application.DTOs.Permissions
{
    /// <summary>
    /// Represents a permission.
    /// </summary>
    /// <param name="Id">The permission identifier.</param>
    /// <param name="DisplayName">The permission display name.</param>
    public record PermissionResponse(
        Guid Id,
        string DisplayName);
}
