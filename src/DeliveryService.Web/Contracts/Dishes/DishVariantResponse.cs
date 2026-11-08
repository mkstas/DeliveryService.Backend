namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Price"></param>
    /// <param name="ImageUrl"></param>
    /// <param name="Specifications"></param>
    public record DishVariantResponse(
        Guid Id,
        decimal Price,
        string? ImageUrl,
        List<DishSpecificationResponse> Specifications);
}
