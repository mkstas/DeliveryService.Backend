using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface ICartItemRepository
    {
        Task CreateCartItemAsync(CartItem cartItem);

        Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId);

        Task<List<CartItem>> GetCartItemsAsync();

        Task UpdateCartItemAsync(CartItem cartItem);

        Task RemoveCartItemAsync(CartItem cartItem);
    }
}
