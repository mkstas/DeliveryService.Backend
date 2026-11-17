using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CartItems
{
    public record CreateCartItemRequest(
        Guid DishId,

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        int Quantity);
}
