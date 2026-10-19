namespace DeliveryService.Web.Contracts.Users
{
    public record UserMeResponse(
        Guid Id,
        string EmailAddress,
        string Name,
        string? ImageUrl,
        List<PaymentMethodResponse> PaymentMethods);
}
