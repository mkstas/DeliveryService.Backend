namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    public record DishIngredientResponse(
        Guid Id,
        string Name);
}
