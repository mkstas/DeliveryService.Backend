using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Orders
{
    public record UpdateOrderRequest(
        [Required(ErrorMessage = "Status is required.")]
        string Status,

        [Required(ErrorMessage = "Address is required.")]
        string Address,

        decimal Cost,
        string? Details = null);
}
