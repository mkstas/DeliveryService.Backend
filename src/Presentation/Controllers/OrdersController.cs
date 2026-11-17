using Application.Abstractions.Services;
using Application.DTOs.Orders;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Orders.Create)]
        public async Task<ActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            await orderService.CreateOrderAsync(request);

            return Created();
        }

        [HttpGet]
        [HasPermission(Permissions.Orders.Read)]
        public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrdersAsync()
        {
            var orders = await orderService.GetOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{orderId:guid}")]
        [HasPermission(Permissions.Orders.Read)]
        public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] Guid orderId)
        {
            var order = await orderService.GetOrderByIdAsync(orderId);

            return Ok(order);
        }

        [HttpPut("{orderId:guid}")]
        [HasPermission(Permissions.Orders.Update)]
        public async Task<ActionResult> UpdateOrderAsync(
            [FromRoute] Guid orderId,
            [FromBody] UpdateOrderRequest request)
        {
            await orderService.UpdateOrderAsync(orderId, request);

            return Ok();
        }

        [HttpDelete("{orderId:guid}")]
        [HasPermission(Permissions.Orders.Delete)]
        public async Task<ActionResult> RemoveOrderAsync([FromRoute] Guid orderId)
        {
            await orderService.RemoveOrderAsync(orderId);

            return NoContent();
        }
    }
}
