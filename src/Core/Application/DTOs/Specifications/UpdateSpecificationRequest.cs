using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Specifications
{
    /// <summary>
    /// Represents the data required to update an existing specification.
    /// </summary>
    /// <param name="Name">The specification new name.</param>
    public record UpdateSpecificationRequest(
        [Required(ErrorMessage = "Name is required.")]
        string Name);
}
