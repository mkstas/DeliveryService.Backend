using Application.DTOs.Orders;

namespace Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderRequest request);

        Task<IReadOnlyList<OrderResponse>> GetOrdersAsync();

        Task<OrderResponse> GetOrderByIdAsync(Guid orderId);

        Task UpdateOrderAsync(Guid orderId, UpdateOrderRequest request);

        Task RemoveOrderAsync(Guid orderId);
    }
}
