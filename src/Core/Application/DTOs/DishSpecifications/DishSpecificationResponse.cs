namespace Application.DTOs.DishSpecifications
{
    public record DishSpecificationResponse(
        Guid Id,
        Guid DishId,
        Guid SpecificationId,
        string Value);
}
