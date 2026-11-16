namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents a user role.
    /// </summary>
    /// <param name="Id">The role identifier.</param>
    /// <param name="Name">The role name.</param>
    public record UserRoleResponse(
        Guid Id,
        string Name);
}
