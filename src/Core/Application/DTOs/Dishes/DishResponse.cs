using Application.DTOs.DishSpecifications;

namespace Application.DTOs.Dishes
{
    public record DishResponse(
        Guid Id,
        Guid RestaurantId,
        Guid CategoryId,
        string Name,
        decimal Price,
        string? Description,
        string? ImageUrl,
        IReadOnlyList<DishSpecificationResponse> Specifications);
}
