namespace DeliveryService.Web.Contracts.Modifiers
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Price"></param>
    /// <param name="ImageUrl"></param>
    public record ModifierResponse(
        Guid Id,
        string Name,
        decimal Price,
        string? ImageUrl);
}
