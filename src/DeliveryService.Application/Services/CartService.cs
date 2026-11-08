using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;

namespace DeliveryService.Application.Services
{
    public class CartService(IUserRepository userRepository) : ICartService
    {
        public async Task<Cart> GetCartAsync(Guid userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);

            if (user.Cart is null)
            {
                throw new NotFoundException<Cart>($"Cart with user {userId}");
            }

            return user.Cart;
        }

        public async Task CreateCartItem(Guid userId, Guid dishVariantId, List<Modifier> modifiers, uint quantity)
        {
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);

            user.CreateCart();
            user.Cart?.IncreaseCartItemQuantity(dishVariantId, modifiers, quantity);

            await userRepository.UpdateUserAsync(user);
        }

        public async Task RemoveCartItem(Guid userId, Guid cartItemId, uint quantity)
        {
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);

            user.Cart?.DecreaseCartItemQuantity(cartItemId, quantity);

            await userRepository.UpdateUserAsync(user);
        }
    }
}
