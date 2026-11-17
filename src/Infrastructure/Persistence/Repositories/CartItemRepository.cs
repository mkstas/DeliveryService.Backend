using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class CartItemRepository(PersistenceDbContext context) : ICartItemRepository
    {
        public async Task CreateCartItemAsync(CartItem cartItem)
        {
            context.CartItems.Add(cartItem);
            await context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsAsync(Guid userId)
        {
            return await context.CartItems
                .Include(ci => ci.Dish)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        public async Task<CartItem?> GetCartItemByIdAsync(Guid userId, Guid cartItemId)
        {
            return await context.CartItems
                .Include(ci => ci.Dish)
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.Id == cartItemId);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            await context.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(CartItem cartItem)
        {
            context.CartItems.Remove(cartItem);
            await context.SaveChangesAsync();
        }
    }
}
