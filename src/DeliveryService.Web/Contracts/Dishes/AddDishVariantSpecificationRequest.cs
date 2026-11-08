using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="DishId"></param>
    /// <param name="DishVariantId"></param>
    /// <param name="SpecificationId"></param>
    /// <param name="Value"></param>
    public record AddDishVariantSpecificationRequest(
        [Required(ErrorMessage = "Dish id is required.")]
        Guid DishId,

        [Required(ErrorMessage = "Dish varinat id is required.")]
        Guid DishVariantId,

        [Required(ErrorMessage = "Specification id is required.")]
        Guid SpecificationId,

        [Required(ErrorMessage = "Value is required.")]
        [MaxLength(
            StringBounded.MAX_LENGTH,
            ErrorMessage = "Value cannot exceed 256 characters.")]
        string Value);
}
