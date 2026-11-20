using Application.Abstractions.Services;
using Application.DTOs.CartItems;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing cart items.
    /// </summary>
    /// <param name="cartItemService">The cart item service.</param>
    [ApiController]
    [Route("[controller]")]
    public class CartItemsController(ICartItemService cartItemService) : ControllerBase
    {
        /// <summary>
        /// Creates a cart item for the current user.
        /// </summary>
        /// <param name="request">The request to create cart item.</param>
        /// <returns>An <see cref="ActionResult"/> representing a created response.</returns>
        [HttpPost]
        [HasPermission(Permissions.CartItems.Create)]
        public async Task<ActionResult> CreateCartItemAsync([FromBody] CreateCartItemRequest request)
        {
            await cartItemService.CreateCartItemAsync(User.GetCurrentId(), request);

            return Created();
        }

        /// <summary>
        /// Returns the cart items of the current user.
        /// </summary>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the cart items.</returns>
        [HttpGet]
        [HasPermission(Permissions.CartItems.Read)]
        public async Task<ActionResult<IReadOnlyList<CartItemResponse>>> GetCartItemsAsync()
        {
            var cartItems = await cartItemService.GetCartItemsAsync(User.GetCurrentId());

            return Ok(cartItems);
        }

        /// <summary>
        /// Returns a cart item by its identifier.
        /// </summary>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the cart item.</returns>
        [HttpGet("{cartItemId:guid}")]
        [HasPermission(Permissions.CartItems.Read)]
        public async Task<ActionResult<CartItemResponse>> GetCartItemById([FromRoute] Guid cartItemId)
        {
            var cartItem = await cartItemService.GetCartItemByIdAsync(User.GetCurrentId(), cartItemId);

            return Ok(cartItem);
        }

        /// <summary>
        /// Updates the specified cart item of the current user.
        /// </summary>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <param name="request">The request to update cart item.</param>
        /// <returns>An <see cref="ActionResult"/> representing an OK response.</returns>
        [HttpPut("{cartItemId:guid}")]
        [HasPermission(Permissions.CartItems.Update)]
        public async Task<ActionResult> UpdateCartItemAsync(
            [FromRoute] Guid cartItemId,
            [FromBody] UpdateCartItemRequest request)
        {
            await cartItemService.UpdateCartItemAsync(User.GetCurrentId(), cartItemId, request);

            return Ok();
        }

        /// <summary>
        /// Removes the specified cart item of the current user.
        /// </summary>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns>An <see cref="ActionResult"/> representing a no-content response.</returns>
        [HttpDelete("{cartItemId:guid}")]
        [HasPermission(Permissions.CartItems.Delete)]
        public async Task<ActionResult> RemoveCartItemAsync([FromRoute] Guid cartItemId)
        {
            await cartItemService.RemoveCartItemAsync(User.GetCurrentId(), cartItemId);

            return NoContent();
        }
    }
}
