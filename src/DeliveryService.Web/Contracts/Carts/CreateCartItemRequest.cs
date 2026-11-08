using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.Entities;

namespace DeliveryService.Web.Contracts.Carts
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="DishVariantId"></param>
    /// <param name="Quantity"></param>
    /// <param name="Modifiers"></param>
    public record CreateCartItemRequest(
        [Required(ErrorMessage = "Dish varinat id is required.")]
        Guid DishVariantId,

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, uint.MaxValue, ErrorMessage = "Quantity cannot be less 1.")]
        uint Quantity,

        [Required(ErrorMessage = "Modifiers are required.")]
        List<Modifier> Modifiers);
}
