using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Specifications
{
    /// <summary>
    /// Represents the data required to create a new specification.
    /// </summary>
    /// <param name="Name">The specification name.</param>
    public record CreateSpecificationRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
