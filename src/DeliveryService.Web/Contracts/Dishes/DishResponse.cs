using DeliveryService.Web.Contracts.Categories;
using DeliveryService.Web.Contracts.Ingredients;
using DeliveryService.Web.Contracts.Modifiers;

namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Establishment"></param>
    /// <param name="Category"></param>
    /// <param name="Ingredients"></param>
    /// <param name="Modifiers"></param>
    /// <param name="Variants"></param>
    public record DishResponse(
        Guid Id,
        string Name,
        string? Description,
        DishEstablishmentResponse Establishment,
        CategoryResponse Category,
        List<IngredientResponse> Ingredients,
        List<ModifierResponse> Modifiers,
        List<DishVariantResponse> Variants);
}
