namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents a user payment.
    /// </summary>
    /// <param name="Id">The payment identifier.</param>
    /// <param name="CardNumber">The card number of the payment.</param>
    public record PaymentResponse(
        Guid Id,
        string CardNumber);
}
