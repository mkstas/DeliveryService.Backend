using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Carts
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Quantity"></param>
    public record UpdateCartItemRequest(
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, uint.MaxValue, ErrorMessage = "Quantity cannot be less 1.")]
        uint Quantity);
}
