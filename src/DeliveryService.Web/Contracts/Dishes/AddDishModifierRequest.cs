using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="DishId"></param>
    /// <param name="ModifierId"></param>
    public record AddDishModifierRequest(
        [Required(ErrorMessage = "Dish id is required.")]
        Guid DishId,

        [Required(ErrorMessage = "Modifier id is required.")]
        Guid ModifierId);
}
