using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Orders
{
    /// <summary>
    /// Represents the data required to update an order.
    /// </summary>
    /// <param name="Status">The new order status.</param>
    /// <param name="Address">The new delivery address.</param>
    /// <param name="Cost">The new order cost.</param>
    /// <param name="Details">The dishes included in the order.</param>
    /// <param name="Comment">The comment left by the user, if any.</param>
    public record UpdateOrderRequest(
        [Required(ErrorMessage = "Status is required.")]
        string Status,

        [Required(ErrorMessage = "Address is required.")]
        string Address,

        decimal Cost,
        IReadOnlyList<OrderDish>? Details = null,
        string? Comment = null);
}
