namespace DeliveryService.Web.Contracts.Categories
{
    /// <summary>
    /// Represents a category.
    /// </summary>
    /// <param name="Id">The category's identifier.</param>
    /// <param name="Name">The category's name.</param>
    public record CategoryResponse(
        Guid Id,
        string Name);
}
