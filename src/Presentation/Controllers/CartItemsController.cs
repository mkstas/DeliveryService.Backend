using Application.Abstractions.Services;
using Application.DTOs.CartItems;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartItemsController(ICartItemService cartItemService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.CartItems.Create)]
        public async Task<ActionResult> CreateCartItemAsync([FromBody] CreateCartItemRequest request)
        {
            await cartItemService.CreateCartItemAsync(request);

            return Created();
        }

        [HttpGet]
        [HasPermission(Permissions.CartItems.Read)]
        public async Task<ActionResult<IReadOnlyList<CartItemResponse>>> GetCartItemsAsync()
        {
            var cartItems = await cartItemService.GetCartItemsAsync();

            return Ok(cartItems);
        }

        [HttpGet("{cartItemId:guid}")]
        [HasPermission(Permissions.CartItems.Read)]
        public async Task<ActionResult<CartItemResponse>> GetCartItemById([FromRoute] Guid cartItemId)
        {
            var cartItem = await cartItemService.GetCartItemByIdAsync(cartItemId);

            return Ok(cartItem);
        }

        [HttpPut("{cartItemId:guid}")]
        [HasPermission(Permissions.CartItems.Update)]
        public async Task<ActionResult> UpdateCartItemAsync(
            [FromRoute] Guid cartItemId,
            [FromBody] UpdateCartItemRequest request)
        {
            await cartItemService.UpdateCartItemAsync(cartItemId, request);

            return Ok();
        }

        [HttpDelete("{cartItemId:guid}")]
        [HasPermission(Permissions.CartItems.Delete)]
        public async Task<ActionResult> RemoveCartItemAsync([FromRoute] Guid cartItemId)
        {
            await cartItemService.RemoveCartItemAsync(cartItemId);

            return NoContent();
        }
    }
}
