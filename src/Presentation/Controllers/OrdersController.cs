using Application.Abstractions.Services;
using Application.DTOs.Orders;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing orders.
    /// </summary>
    /// <param name="orderService">The order service.</param>
    /// <param name="restaurantService">The restaurant service.</param>
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(IOrderService orderService, IRestaurantService restaurantService) : ControllerBase
    {
        /// <summary>
        /// Creates a new order for the current client.
        /// </summary>
        /// <param name="request">The order creation request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost]
        [HasPermission(Permissions.Orders.Create)]
        public async Task<ActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            await orderService.CreateOrderAsync(User.GetCurrentId(), request);

            return Created();
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with all orders.</returns>
        [HttpGet]
        [HasPermission(Permissions.Orders.Read)]
        public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrdersAsync()
        {
            var orders = await orderService.GetOrdersAsync();

            return Ok(orders);
        }

        /// <summary>
        /// Retrieves the orders that contain dishes of the restaurant assigned to the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the matching orders.</returns>
        [HttpGet("me")]
        [HasPermission(Permissions.Orders.Read)]
        public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetMyRestaurantOrdersAsync()
        {
            var restaurant = await restaurantService.GetUserRestaurantAsync(User.GetCurrentId());

            var orders = await orderService.GetOrdersByRestaurantAsync(restaurant.Id);

            return Ok(orders);
        }

        /// <summary>
        /// Retrieves an order by its identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the matching order.</returns>
        [HttpGet("{orderId:guid}")]
        [HasPermission(Permissions.Orders.Read)]
        public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] Guid orderId)
        {
            var order = await orderService.GetOrderByIdAsync(orderId);

            return Ok(order);
        }

        /// <summary>
        /// Changes the status of an existing order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="request">The status update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPatch("{orderId:guid}/status")]
        [HasPermission(Permissions.Orders.UpdateStatus)]
        public async Task<ActionResult> UpdateOrderStatusAsync(
            [FromRoute] Guid orderId,
            [FromBody] UpdateOrderStatusRequest request)
        {
            await orderService.UpdateOrderStatusAsync(orderId, request);

            return Ok();
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="request">The order update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("{orderId:guid}")]
        [HasPermission(Permissions.Orders.Update)]
        public async Task<ActionResult> UpdateOrderAsync(
            [FromRoute] Guid orderId,
            [FromBody] UpdateOrderRequest request)
        {
            await orderService.UpdateOrderAsync(orderId, request);

            return Ok();
        }

        /// <summary>
        /// Removes an order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{orderId:guid}")]
        [HasPermission(Permissions.Orders.Delete)]
        public async Task<ActionResult> RemoveOrderAsync([FromRoute] Guid orderId)
        {
            await orderService.RemoveOrderAsync(orderId);

            return NoContent();
        }
    }
}
