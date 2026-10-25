namespace DeliveryService.Web.Contracts.Permissions
{
    /// <summary>
    /// Represents a permission.
    /// </summary>
    /// <param name="Id">The permission's identifier.</param>
    /// <param name="DisplayName">The permission's display name.</param>
    public record PermissionResponse(
        Guid Id,
        string DisplayName);
}
