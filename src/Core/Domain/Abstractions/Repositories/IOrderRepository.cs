using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to orders.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateOrderAsync(Order order);

        /// <summary>
        /// Returns an order by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>The matching order, or <c>null</c> if none found.</returns>
        Task<Order?> GetOrderByIdAsync(Guid orderId);

        /// <summary>
        /// Returns all orders in the system.
        /// </summary>
        /// <returns>All orders in the system.</returns>
        Task<List<Order>> GetOrdersAsync();

        /// <summary>
        /// Updates the specified order.
        /// </summary>
        /// <param name="order">The order to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateOrderAsync(Order order);

        /// <summary>
        /// Removes the specified order.
        /// </summary>
        /// <param name="order">The order to remove.</param>
        /// <returns>A task that represents the asynchronous remove operation.</returns>
        Task RemoveOrderAsync(Order order);
    }
}
