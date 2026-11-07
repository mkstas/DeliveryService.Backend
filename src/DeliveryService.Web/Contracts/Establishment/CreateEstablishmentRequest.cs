using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Establishment
{
    /// <summary>
    /// Represents the data required to create a new establishment.
    /// </summary>
    /// <param name="Name">The establishment's name.</param>
    /// <param name="BusinessAccount">The establishment's business bank account.</param>
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
