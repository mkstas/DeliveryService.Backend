using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.Common;
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
        public async Task CreateOrderAsync(Guid userId, CreateOrderRequest request)
        {
            _ = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            await orderRepository.CreateOrderAsync(Order.Create(
                userId,
                request.Status,
                request.Address,
                request.Cost,
                OrderDetailsSerializer.Serialize(request.Details),
                request.Comment));

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
            order.ChangeDetails(OrderDetailsSerializer.Serialize(request.Details));
            order.ChangeComment(request.Comment);

            await orderRepository.UpdateOrderAsync(order);
            await cacheService.RemoveAsync(CacheKeys.Orders.All);
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusRequest request)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId)
                ?? throw new NotFoundException<Order>(orderId);

            order.ChangeStatus(request.Status);

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
                OrderDetailsSerializer.Deserialize(order.Details),
                order.Comment);
        }
    }
}
