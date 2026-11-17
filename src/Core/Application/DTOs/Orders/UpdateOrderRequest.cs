using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Orders
{
    /// <summary>
    /// Represents the data required to update an order.
    /// </summary>
    /// <param name="Status">The new order status.</param>
    /// <param name="Address">The new delivery address.</param>
    /// <param name="Cost">The new order cost.</param>
    /// <param name="Details">The additional order details, if any.</param>
    public record UpdateOrderRequest(
        [Required(ErrorMessage = "Status is required.")]
        string Status,

        [Required(ErrorMessage = "Address is required.")]
        string Address,

        decimal Cost,
        string? Details = null);
}
