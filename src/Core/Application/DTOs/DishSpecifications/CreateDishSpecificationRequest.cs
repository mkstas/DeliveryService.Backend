using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.DishSpecifications
{
    /// <summary>
    /// Represents the data required to create a new dish specification.
    /// </summary>
    /// <param name="DishId">The dish identifier.</param>
    /// <param name="SpecificationId">The specification identifier.</param>
    /// <param name="Value">The dish specification value.</param>
    public record CreateDishSpecificationRequest(
        Guid DishId,
        Guid SpecificationId,

        [Required(ErrorMessage = "Value is required.")]
        string Value);
}
