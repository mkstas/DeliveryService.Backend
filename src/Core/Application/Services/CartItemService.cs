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
        public async Task CreateCartItemAsync(CreateCartItemRequest request)
        {
            _ = await dishRepository.GetDishByIdAsync(request.DishId)
                ?? throw new NotFoundException<Dish>(request.DishId);

            await cartItemRepository.CreateCartItemAsync(CartItem.Create(request.DishId, request.Quantity));
            await cacheService.RemoveAsync(CacheKeys.CartItems.All);
        }

        public async Task<IReadOnlyList<CartItemResponse>> GetCartItemsAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<CartItemResponse>>(CacheKeys.CartItems.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var cartItems = await cartItemRepository.GetCartItemsAsync();

            IReadOnlyList<CartItemResponse> response = [.. cartItems.Select(MapToResponse)];

            await cacheService.SetAsync(CacheKeys.CartItems.All, response, TimeSpan.FromHours(1));

            return response;
        }

        public async Task<CartItemResponse> GetCartItemByIdAsync(Guid cartItemId)
        {
            var cartItem = await cartItemRepository.GetCartItemByIdAsync(cartItemId)
                ?? throw new NotFoundException<CartItem>(cartItemId);

            return MapToResponse(cartItem);
        }

        public async Task UpdateCartItemAsync(Guid cartItemId, UpdateCartItemRequest request)
        {
            var cartItem = await cartItemRepository.GetCartItemByIdAsync(cartItemId)
                ?? throw new NotFoundException<CartItem>(cartItemId);

            cartItem.ChangeQuantity(request.Quantity);

            await cartItemRepository.UpdateCartItemAsync(cartItem);
            await cacheService.RemoveAsync(CacheKeys.CartItems.All);
        }

        public async Task RemoveCartItemAsync(Guid cartItemId)
        {
            var cartItem = await cartItemRepository.GetCartItemByIdAsync(cartItemId)
                ?? throw new NotFoundException<CartItem>(cartItemId);

            await cartItemRepository.RemoveCartItemAsync(cartItem);
            await cacheService.RemoveAsync(CacheKeys.CartItems.All);
        }

        private static CartItemResponse MapToResponse(CartItem cartItem)
        {
            return new CartItemResponse(cartItem.Id, cartItem.DishId, cartItem.Quantity);
        }
    }
}
