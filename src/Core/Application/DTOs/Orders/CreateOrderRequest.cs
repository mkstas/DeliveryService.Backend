using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Orders
{
    /// <summary>
    /// Represents the data required to create a new order.
    /// </summary>
    /// <param name="UserId">The user identifier.</param>
    /// <param name="Status">The order status.</param>
    /// <param name="Address">The delivery address.</param>
    /// <param name="Cost">The order cost.</param>
    /// <param name="Details">The additional order details, if any.</param>
    public record CreateOrderRequest(
        Guid UserId,

        [Required(ErrorMessage = "Status is required.")]
        string Status,

        [Required(ErrorMessage = "Address is required.")]
        string Address,

        decimal Cost,
        string? Details = null);
}
