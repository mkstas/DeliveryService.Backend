using DeliveryService.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    /// Represents the data required to add a payment method to a user.
    /// </summary>
    /// <param name="CardNumber">The card number of the payment method to add.</param>
    public record AddPaymentMethodRequest(
        [Required(ErrorMessage = "Card number is required.")]
        [Length(
            CardNumber.LENGTH,
            CardNumber.LENGTH,
            ErrorMessage = "Card number must 16 characters long.")]
        string CardNumber);
}
