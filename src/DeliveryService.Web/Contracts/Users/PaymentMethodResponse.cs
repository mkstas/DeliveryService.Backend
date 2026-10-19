namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    /// Represents a user's payment method.
    /// </summary>
    /// <param name="Id">The payment method's identifier.</param>
    /// <param name="CardNumber">The card number of the payment method.</param>
    public record PaymentMethodResponse(
        Guid Id,
        string CardNumber);
}
