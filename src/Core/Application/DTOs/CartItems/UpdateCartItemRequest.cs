using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CartItems
{
    /// <summary>
    /// Represents the data required to update an existing cart item.
    /// </summary>
    /// <param name="Quantity">The updated dish quantity.</param>
    public record UpdateCartItemRequest(
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        int Quantity);
}
