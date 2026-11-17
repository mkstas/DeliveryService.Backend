using Application.DTOs.Orders;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing orders.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="request">The request to create order.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateOrderAsync(CreateOrderRequest request);

        /// <summary>
        /// Returns all orders in the system.
        /// </summary>
        /// <returns>All orders in the system.</returns>
        Task<IReadOnlyList<OrderResponse>> GetOrdersAsync();

        /// <summary>
        /// Returns an order by its identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>The matching order.</returns>
        Task<OrderResponse> GetOrderByIdAsync(Guid orderId);

        /// <summary>
        /// Updates the specified order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="request">The request to update order.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateOrderAsync(Guid orderId, UpdateOrderRequest request);

        /// <summary>
        /// Remove the specified order from the system.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveOrderAsync(Guid orderId);
    }
}
