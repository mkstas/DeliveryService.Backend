namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents a user with its assigned roles and payments.
    /// </summary>
    /// <param name="Id">The user identifier.</param>
    /// <param name="EmailAddress">The user email address.</param>
    /// <param name="Name">The user name.</param>
    /// <param name="ImageUrl">The URL of the user image.</param>
    /// <param name="RestaurantId">The identifier of the restaurant the user belongs to, if any.</param>
    /// <param name="Roles">The roles assigned to the user.</param>
    /// <param name="Payments">The payment methods assigned to the user.</param>
    public record UserResponse(
        Guid Id,
        string EmailAddress,
        string Name,
        string? ImageUrl,
        Guid? RestaurantId,
        IReadOnlyList<UserRoleResponse> Roles,
        IReadOnlyList<PaymentResponse> Payments);
}
