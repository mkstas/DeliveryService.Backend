using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Price"></param>
    public record UpdateDishVariantRequest(
        [Required(ErrorMessage = "Price is required.")]
        decimal Price);
}
