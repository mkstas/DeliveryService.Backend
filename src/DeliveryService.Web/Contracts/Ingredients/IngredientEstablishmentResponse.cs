namespace DeliveryService.Web.Contracts.Ingredients
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    public record IngredientEstablishmentResponse(
        Guid Id,
        string Name);
}
