using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="DishId"></param>
    /// <param name="Price"></param>
    public record CreateDishVariantRequest(
        [Required(ErrorMessage = "Dish id is required.")]
        Guid DishId,

        [Required(ErrorMessage = "Price is required.")]
        decimal Price);
}
