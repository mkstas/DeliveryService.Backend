using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.DishSpecifications
{
    public record CreateDishSpecificationRequest(
        Guid DishId,
        Guid SpecificationId,

        [Required(ErrorMessage = "Value is required.")]
        string Value);
}
