namespace DeliveryService.Web.Contracts.Ingredients
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    public record IngredientResponse(
        Guid Id,
        string Name);
}
