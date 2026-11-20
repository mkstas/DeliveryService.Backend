using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Orders
{
    /// <summary>
    /// Represents the data required to change an order status.
    /// </summary>
    /// <param name="Status">The new order status.</param>
    public record UpdateOrderStatusRequest(
        [Required(ErrorMessage = "Status is required.")]
        string Status);
}
