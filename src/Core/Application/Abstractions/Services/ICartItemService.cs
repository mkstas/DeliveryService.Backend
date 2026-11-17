using Application.DTOs.CartItems;

namespace Application.Abstractions.Services
{
    public interface ICartItemService
    {
        Task CreateCartItemAsync(CreateCartItemRequest request);

        Task<IReadOnlyList<CartItemResponse>> GetCartItemsAsync();

        Task<CartItemResponse> GetCartItemByIdAsync(Guid cartItemId);

        Task UpdateCartItemAsync(Guid cartItemId, UpdateCartItemRequest request);

        Task RemoveCartItemAsync(Guid cartItemId);
    }
}
