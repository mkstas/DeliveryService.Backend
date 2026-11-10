using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Carts;
using DeliveryService.Web.Contracts.Dishes;
using DeliveryService.Web.Contracts.Modifiers;
using DeliveryService.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController(ICartService cartService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Carts.Create)]
        public async Task<IActionResult> CreateCartItemAsync([FromBody] CreateCartItemRequest request)
        {
            await cartService.CreateCartItem(
                User.GetCurrentUserId(), request.DishVariantId, request.Modifiers, request.Quantity);

            return NoContent();
        }

        [HttpGet]
        [HasPermission(Permissions.Carts.Read)]
        public async Task<IActionResult> GetCartAsync()
        {
            var cart = await cartService.GetCartAsync(User.GetCurrentUserId());

            var response = new CartResponse(cart.Id,
                [.. cart.Items.Select(i => new CartItemResponse(i.Id, i.DishVariant.Price, i.Quantity,
                    i.DishVariant.ImageUrl is null ? null : (string)i.DishVariant.ImageUrl,
                    [.. i.Modifiers.Select(m => new ModifierResponse(m.Id, m.Name, m.Price,
                        m.ImageUrl is null ? null : (string)m.ImageUrl))]))]);

            return Ok(response);
        }

        [HttpPatch("{cartItemId:guid}")]
        [HasPermission(Permissions.Carts.Update)]
        public async Task<IActionResult> RemoveCartItemAsync(
            [FromRoute] Guid cartItemId,
            [FromBody] UpdateCartItemRequest request)
        {
            await cartService.RemoveCartItem(User.GetCurrentUserId(), cartItemId, request.Quantity);

            return NoContent();
        }
    }
}
