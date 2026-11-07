namespace DeliveryService.Web.Contracts.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    /// <param name="Id">The specification identifier.</param>
    /// <param name="Name">The specification name.</param>
    public record SpecificationResponse(
        Guid Id,
        string Name);
}
