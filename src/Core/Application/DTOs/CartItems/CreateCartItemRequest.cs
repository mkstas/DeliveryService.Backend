using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CartItems
{
    /// <summary>
    /// Represents the data required to create a new cart item.
    /// </summary>
    /// <param name="DishId">The dish identifier.</param>
    /// <param name="Quantity">The dish quantity.</param>
    public record CreateCartItemRequest(
        Guid DishId,

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        int Quantity);
}
