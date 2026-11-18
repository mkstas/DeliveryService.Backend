using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Orders
{
    /// <summary>
    /// Represents the data required to create a new order.
    /// </summary>
    /// <param name="Status">The order status.</param>
    /// <param name="Address">The delivery address.</param>
    /// <param name="Cost">The order cost.</param>
    /// <param name="Details">The dishes included in the order.</param>
    /// <param name="Comment">The comment left by the user, if any.</param>
    public record CreateOrderRequest(
        [Required(ErrorMessage = "Status is required.")]
        string Status,

        [Required(ErrorMessage = "Address is required.")]
        string Address,

        decimal Cost,
        IReadOnlyList<OrderDish>? Details = null,
        string? Comment = null);
}
