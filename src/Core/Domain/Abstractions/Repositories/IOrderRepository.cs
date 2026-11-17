using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);

        Task<Order?> GetOrderByIdAsync(Guid orderId);

        Task<List<Order>> GetOrdersAsync();

        Task UpdateOrderAsync(Order order);

        Task RemoveOrderAsync(Order order);
    }
}
