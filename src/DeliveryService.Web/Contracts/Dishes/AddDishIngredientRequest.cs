using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="DishId"></param>
    /// <param name="IngredientId"></param>
    public record AddDishIngredientRequest(
        [Required(ErrorMessage = "Dish id is required.")]
        Guid DishId,

        [Required(ErrorMessage = "Ingredient id is required.")]
        Guid IngredientId);
}
