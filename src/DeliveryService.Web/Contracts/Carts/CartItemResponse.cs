using DeliveryService.Web.Contracts.Modifiers;

namespace DeliveryService.Web.Contracts.Carts
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Price"></param>
    /// <param name="Quantity"></param>
    /// <param name="ImageUrl"></param>
    /// <param name="Modifiers"></param>
    public record CartItemResponse(
        Guid Id,
        decimal Price,
        uint Quantity,
        string? ImageUrl,
        List<ModifierResponse> Modifiers);
}
