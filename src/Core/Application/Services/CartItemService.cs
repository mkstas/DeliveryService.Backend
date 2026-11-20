using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.CartItems;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    internal class CartItemService(
        ICartItemRepository cartItemRepository,
        IDishRepository dishRepository,
        ICacheService cacheService) : ICartItemService
    {
        public async Task CreateCartItemAsync(Guid userId, CreateCartItemRequest request)
        {
            _ = await dishRepository.GetDishByIdAsync(request.DishId)
                ?? throw new NotFoundException<Dish>(request.DishId);

            await cartItemRepository.CreateCartItemAsync(CartItem.Create(userId, request.DishId, request.Quantity));
            await cacheService.RemoveAsync(CacheKeys.CartItems.ByUser(userId));
        }

        public async Task<IReadOnlyList<CartItemResponse>> GetCartItemsAsync(Guid userId)
        {
            var cacheKey = CacheKeys.CartItems.ByUser(userId);
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<CartItemResponse>>(cacheKey);

            if (cachedResponse is not null)
                return cachedResponse;

            var cartItems = await cartItemRepository.GetCartItemsAsync(userId);

            IReadOnlyList<CartItemResponse> response = [.. cartItems.Select(MapToResponse)];

            await cacheService.SetAsync(cacheKey, response, TimeSpan.FromHours(1));

            return response;
        }

        public async Task<CartItemResponse> GetCartItemByIdAsync(Guid userId, Guid cartItemId)
        {
            var cartItem = await cartItemRepository.GetCartItemByIdAsync(userId, cartItemId)
                ?? throw new NotFoundException<CartItem>(cartItemId);

            return MapToResponse(cartItem);
        }

        public async Task UpdateCartItemAsync(Guid userId, Guid cartItemId, UpdateCartItemRequest request)
        {
            var cartItem = await cartItemRepository.GetCartItemByIdAsync(userId, cartItemId)
                ?? throw new NotFoundException<CartItem>(cartItemId);

            cartItem.ChangeQuantity(request.Quantity);

            await cartItemRepository.UpdateCartItemAsync(cartItem);
            await cacheService.RemoveAsync(CacheKeys.CartItems.ByUser(userId));
        }

        public async Task RemoveCartItemAsync(Guid userId, Guid cartItemId)
        {
            var cartItem = await cartItemRepository.GetCartItemByIdAsync(userId, cartItemId)
                ?? throw new NotFoundException<CartItem>(cartItemId);

            await cartItemRepository.RemoveCartItemAsync(cartItem);
            await cacheService.RemoveAsync(CacheKeys.CartItems.ByUser(userId));
        }

        private static CartItemResponse MapToResponse(CartItem cartItem)
        {
            return new CartItemResponse(cartItem.Id, cartItem.DishId, cartItem.Quantity);
        }
    }
}
