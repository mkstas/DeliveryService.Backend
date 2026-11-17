namespace Application.DTOs.DishSpecifications
{
    /// <summary>
    /// Represents a dish specification.
    /// </summary>
    /// <param name="Id">The dish specification identifier.</param>
    /// <param name="DishId">The dish identifier.</param>
    /// <param name="SpecificationId">The specification identifier.</param>
    /// <param name="Value">The dish specification value.</param>
    public record DishSpecificationResponse(
        Guid Id,
        Guid DishId,
        Guid SpecificationId,
        string Value);
}
