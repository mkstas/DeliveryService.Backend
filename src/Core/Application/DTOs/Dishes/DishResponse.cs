using Application.DTOs.DishSpecifications;

namespace Application.DTOs.Dishes
{
    /// <summary>
    /// Represents a dish.
    /// </summary>
    /// <param name="Id">The dish identifier.</param>
    /// <param name="RestaurantId">The restaurant identifier.</param>
    /// <param name="CategoryId">The category identifier.</param>
    /// <param name="Name">The dish name.</param>
    /// <param name="Price">The dish price.</param>
    /// <param name="Description">The dish description.</param>
    /// <param name="ImageUrl">The dish image URL.</param>
    /// <param name="Specifications">The dish specifications.</param>
    /// <param name="Ingredients">The ingredients included in the dish.</param>
    public record DishResponse(
        Guid Id,
        Guid RestaurantId,
        Guid CategoryId,
        string Name,
        decimal Price,
        string? Description,
        string? ImageUrl,
        IReadOnlyList<DishSpecificationResponse> Specifications,
        IReadOnlyList<DishIngredientResponse> Ingredients);
}
