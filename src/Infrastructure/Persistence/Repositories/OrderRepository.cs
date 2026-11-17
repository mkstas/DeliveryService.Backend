using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class OrderRepository(PersistenceDbContext context) : IOrderRepository
    {
        public async Task CreateOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await context.Orders
                .Include(o => o.User)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await context.SaveChangesAsync();
        }

        public async Task RemoveOrderAsync(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }
}
