using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    /// Represents the data required to create a new dish.
    /// </summary>
    /// <param name="EstablishmentId"></param>
    /// <param name="CategoryId"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public record CreateDishRequest(
        [Required(ErrorMessage = "Establishment id is required.")]
        Guid EstablishmentId,

        [Required(ErrorMessage = "Category id is required.")]
        Guid CategoryId,

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Name cannot exceed 256 characters.")]
        string Name,

        string? Description);
}
