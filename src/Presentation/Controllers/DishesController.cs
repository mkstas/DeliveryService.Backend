using Application.Abstractions.Services;
using Application.DTOs.Dishes;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing dishes.
    /// </summary>
    /// <param name="dishService">The dish service.</param>
    [ApiController]
    [Route("[controller]")]
    public class DishesController(IDishService dishService) : ControllerBase
    {
        /// <summary>
        /// Creates a new dish.
        /// </summary>
        /// <param name="request">The request to create dish.</param>
        /// <returns>An <see cref="ActionResult"/> representing a created response.</returns>
        [HttpPost]
        [HasPermission(Permissions.Dishes.Create)]
        public async Task<ActionResult> CreateDishAsync([FromBody] CreateDishRequest request)
        {
            await dishService.CreateDishAsync(request);

            return Created();
        }

        /// <summary>
        /// Returns all dishes in the system.
        /// </summary>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the dishes.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DishResponse>>> GetDishesAsync()
        {
            var dishes = await dishService.GetDishesAsync();

            return Ok(dishes);
        }

        /// <summary>
        /// Returns a dish by its identifier.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the dish.</returns>
        [HttpGet("{dishId:guid}")]
        public async Task<ActionResult<DishResponse>> GetDishById([FromRoute] Guid dishId)
        {
            var dish = await dishService.GetDishByIdAsync(dishId);

            return Ok(dish);
        }

        /// <summary>
        /// Updates the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="request">The request to update dish.</param>
        /// <returns>An <see cref="ActionResult"/> representing an OK response.</returns>
        [HttpPut("{dishId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<ActionResult> UpdateDishAsync(
            [FromRoute] Guid dishId,
            [FromBody] UpdateDishRequest request)
        {
            await dishService.UpdateDishAsync(dishId, request);

            return Ok();
        }

        /// <summary>
        /// Removes the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>An <see cref="ActionResult"/> representing a no-content response.</returns>
        [HttpDelete("{dishId:guid}")]
        [HasPermission(Permissions.Dishes.Delete)]
        public async Task<ActionResult> RemoveDishAsync([FromRoute] Guid dishId)
        {
            await dishService.RemoveDishAsync(dishId);

            return NoContent();
        }
    }
}
