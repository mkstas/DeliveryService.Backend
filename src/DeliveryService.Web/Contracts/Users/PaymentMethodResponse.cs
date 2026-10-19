namespace DeliveryService.Web.Contracts.Users
{
    public record PaymentMethodResponse(
        Guid Id,
        string CardNumber);
}
