namespace DeliveryService.Web.Contracts.Permissions
{
    /// <summary>
    /// Represents a permission.
    /// </summary>
    /// <param name="Id">The permission's identifier.</param>
    /// <param name="Name">The permission's name.</param>
    public record PermissionResponse(
        Guid Id,
        string Name);
}
