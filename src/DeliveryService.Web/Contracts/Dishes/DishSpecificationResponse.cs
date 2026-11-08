namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Value"></param>
    public record DishSpecificationResponse(
        Guid Id,
        string Name,
        string Value);
}
