using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.DishSpecifications
{
    /// <summary>
    /// Represents the data required to update a dish specification.
    /// </summary>
    /// <param name="Value">The new dish specification value.</param>
    public record UpdateDishSpecificationRequest(
        [Required(ErrorMessage = "Value is required.")]
        string Value);
}
