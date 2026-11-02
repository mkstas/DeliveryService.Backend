using DeliveryService.Web.Contracts.Roles;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    /// Represents a user in the users list.
    /// </summary>
    /// <param name="Id">The user's identifier.</param>
    /// <param name="Name">The user's name.</param>
    /// <param name="EmailAddress">The user's email address.</param>
    /// <param name="ImageUrl">The URL of the user's image.</param>
    /// <param name="Roles">The roles assigned to the user.</param>
    public record UserListResponse(
        Guid Id,
        string Name,
        string EmailAddress,
        string? ImageUrl,
        List<RoleResponse> Roles);
}
