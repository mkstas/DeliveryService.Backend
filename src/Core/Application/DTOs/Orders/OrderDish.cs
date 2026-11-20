using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Orders
{
    /// <summary>
    /// Represents a dish included in an order.
    /// </summary>
    /// <param name="DishId">The dish identifier.</param>
    /// <param name="Quantity">The dish quantity.</param>
    public record OrderDish(
        Guid DishId,

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive.")]
        int Quantity);
}
