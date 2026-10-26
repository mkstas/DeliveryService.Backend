using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Establishment
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="BusinessAccount"></param>
    public record CreateEstablishmentRequest(
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name,

        [Required(ErrorMessage = "Name is required.")]
        [Length(
            BankAccount.LENGTH,
            BankAccount.LENGTH,
            ErrorMessage = "Business account must 20 characters long.")]
        string BusinessAccount);
}
