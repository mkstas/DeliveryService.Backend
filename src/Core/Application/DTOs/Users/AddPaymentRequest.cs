using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users
{
    /// <summary>
    /// Represents the data required to add a payment to a user.
    /// </summary>
    /// <param name="CardNumber">The card number of the payment to add.</param>
    public record AddPaymentRequest(
        [Required(ErrorMessage = "Card number is required.")]
        [Length(
            Payment.CARD_NUMBER_LENGTH,
            Payment.CARD_NUMBER_LENGTH,
            ErrorMessage = "Card number must 16 characters long.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Card number must contain only digits.")]
        string CardNumber);
}
