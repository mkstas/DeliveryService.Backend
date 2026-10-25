using DeliveryService.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="CardNumber"></param>
    public record AddPaymentMethodRequest(
        [Required(ErrorMessage = "Card number is required.")]
        [Length(
            CardNumber.LENGTH,
            CardNumber.LENGTH,
            ErrorMessage = "Card number must 16 characters long.")]
        string CardNumber);
}
