using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.DishSpecifications
{
    public record UpdateDishSpecificationRequest(
        [Required(ErrorMessage = "Value is required.")]
        string Value);
}
