namespace DeliveryService.Web.Contracts.Dishes
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    public record DishEstablishmentResponse(
        Guid Id,
        string Name);
}
