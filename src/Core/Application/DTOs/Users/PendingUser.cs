namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents a user registration that has not yet been confirmed by email.
    /// </summary>
    /// <param name="EmailAddress">The user email address.</param>
    /// <param name="PasswordHash">The hashed user password.</param>
    /// <param name="Name">The user name.</param>
    /// <param name="RoleIds">The roles to assign to the user once the email is confirmed.</param>
    internal sealed record PendingUser(
        string EmailAddress,
        string PasswordHash,
        string Name,
        IReadOnlyList<Guid> RoleIds);
}
