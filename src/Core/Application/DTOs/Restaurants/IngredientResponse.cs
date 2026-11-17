namespace Application.DTOs.Restaurants
{
    public record IngredientResponse(
        Guid Id,
        Guid RestaurantId,
        string Name);
}
