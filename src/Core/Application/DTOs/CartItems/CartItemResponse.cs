namespace Application.DTOs.CartItems
{
    public record CartItemResponse(
        Guid Id,
        Guid DishId,
        int Quantity);
}
