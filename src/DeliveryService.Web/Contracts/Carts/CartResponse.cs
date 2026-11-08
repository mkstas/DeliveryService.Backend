namespace DeliveryService.Web.Contracts.Carts
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="CartItems"></param>
    public record CartResponse(
        Guid Id,
        List<CartItemResponse> CartItems);
}
