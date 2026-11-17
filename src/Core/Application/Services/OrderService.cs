using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.Orders;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    internal class OrderService(
        IOrderRepository orderRepository,
        IUserRepository userRepository,
        ICacheService cacheService) : IOrderService
    {
        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            _ = await userRepository.GetUserByIdAsync(request.UserId)
                ?? throw new NotFoundException<User>(request.UserId);

            await orderRepository.CreateOrderAsync(Order.Create(
                request.UserId,
                request.Status,
                request.Address,
                request.Cost,
                request.Details));

            await cacheService.RemoveAsync(CacheKeys.Orders.All);
        }

        public async Task<IReadOnlyList<OrderResponse>> GetOrdersAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<OrderResponse>>(CacheKeys.Orders.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var orders = await orderRepository.GetOrdersAsync();

            IReadOnlyList<OrderResponse> response = [.. orders.Select(MapToResponse)];

            await cacheService.SetAsync(CacheKeys.Orders.All, response, TimeSpan.FromHours(1));

            return response;
        }

        public async Task<OrderResponse> GetOrderByIdAsync(Guid orderId)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId)
                ?? throw new NotFoundException<Order>(orderId);

            return MapToResponse(order);
        }

        public async Task UpdateOrderAsync(Guid orderId, UpdateOrderRequest request)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId)
                ?? throw new NotFoundException<Order>(orderId);

            order.ChangeStatus(request.Status);
            order.ChangeAddress(request.Address);
            order.ChangeCost(request.Cost);
            order.ChangeDetails(request.Details);

            await orderRepository.UpdateOrderAsync(order);
            await cacheService.RemoveAsync(CacheKeys.Orders.All);
        }

        public async Task RemoveOrderAsync(Guid orderId)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId)
                ?? throw new NotFoundException<Order>(orderId);

            await orderRepository.RemoveOrderAsync(order);
            await cacheService.RemoveAsync(CacheKeys.Orders.All);
        }

        private static OrderResponse MapToResponse(Order order)
        {
            return new OrderResponse(
                order.Id,
                order.UserId,
                order.Status,
                order.Address,
                order.Cost,
                order.Details);
        }
    }
}
