namespace Application.DTOs.CartItems
{
    /// <summary>
    /// Represents a cart item.
    /// </summary>
    /// <param name="Id">The cart item identifier.</param>
    /// <param name="DishId">The dish identifier.</param>
    /// <param name="Quantity">The dish quantity.</param>
    public record CartItemResponse(
        Guid Id,
        Guid DishId,
        int Quantity);
}
