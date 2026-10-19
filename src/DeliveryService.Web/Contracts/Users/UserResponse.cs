using DeliveryService.Web.Contracts.Roles;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    /// Represents the data of the currently authenticated user.
    /// </summary>
    /// <param name="Id">The user's identifier.</param>
    /// <param name="EmailAddress">The user's email address.</param>
    /// <param name="Name">The user's name.</param>
    /// <param name="ImageUrl">The URL of the user's image.</param>
    /// <param name="Roles">The roles assigned to the user.</param>
    /// <param name="PaymentMethods">The user's payment methods.</param>
    public record UserMeResponse(
        Guid Id,
        string EmailAddress,
        string Name,
        string? ImageUrl,
        List<RoleResponse> Roles,
        List<PaymentMethodResponse> PaymentMethods);
}
