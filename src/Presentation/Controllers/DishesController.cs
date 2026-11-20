using Application.Abstractions.Services;
using Application.DTOs.Dishes;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing dishes.
    /// </summary>
    /// <param name="dishService">The dish service.</param>
    /// <param name="restaurantService">The restaurant service.</param>
    [ApiController]
    [Route("[controller]")]
    public class DishesController(IDishService dishService, IRestaurantService restaurantService) : ControllerBase
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
        /// Returns all dishes in the system, optionally filtered by name.
        /// </summary>
        /// <param name="search">Optional substring to match against the dish name.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the dishes.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DishResponse>>> GetDishesAsync([FromQuery] string? search)
        {
            var dishes = await dishService.GetDishesAsync(search);

            return Ok(dishes);
        }

        /// <summary>
        /// Creates a dish in the restaurant assigned to the current user.
        /// </summary>
        /// <param name="request">The request to create dish.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> representing a created response with the dish.</returns>
        [HttpPost("me")]
        [HasPermission(Permissions.Dishes.Create)]
        public async Task<ActionResult<DishResponse>> CreateMyDishAsync([FromBody] CreateDishRequest request)
        {
            var dish = await dishService.CreateUserDishAsync(User.GetCurrentId(), request);

            return Created(string.Empty, dish);
        }

        /// <summary>
        /// Returns the dishes of the restaurant assigned to the current user.
        /// </summary>
        /// <param name="search">Optional substring to match against the dish name.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the dishes.</returns>
        [HttpGet("me")]
        [HasPermission(Permissions.Dishes.Read)]
        public async Task<ActionResult<IReadOnlyList<DishResponse>>> GetMyDishesAsync([FromQuery] string? search)
        {
            var restaurant = await restaurantService.GetUserRestaurantAsync(User.GetCurrentId());

            var dishes = await dishService.GetDishesByRestaurantAsync(restaurant.Id, search);

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
        /// Adds an ingredient to a dish of the restaurant assigned to the current user.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="request">The request to add the ingredient.</param>
        /// <returns>An <see cref="ActionResult"/> representing a created response.</returns>
        [HttpPost("me/{dishId:guid}/ingredients")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<ActionResult> AddMyDishIngredientAsync(
            [FromRoute] Guid dishId,
            [FromBody] AddDishIngredientRequest request)
        {
            await dishService.AddUserDishIngredientAsync(User.GetCurrentId(), dishId, request);

            return Created();
        }

        /// <summary>
        /// Removes an ingredient from a dish of the restaurant assigned to the current user.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="dishIngredientId">The dish ingredient identifier.</param>
        /// <returns>An <see cref="ActionResult"/> representing a no-content response.</returns>
        [HttpDelete("me/{dishId:guid}/ingredients/{dishIngredientId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<ActionResult> RemoveMyDishIngredientAsync(
            [FromRoute] Guid dishId,
            [FromRoute] Guid dishIngredientId)
        {
            await dishService.RemoveUserDishIngredientAsync(User.GetCurrentId(), dishId, dishIngredientId);

            return NoContent();
        }

        /// <summary>
        /// Adds an ingredient to the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="request">The request to add the ingredient.</param>
        /// <returns>An <see cref="ActionResult"/> representing a created response.</returns>
        [HttpPost("{dishId:guid}/ingredients")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<ActionResult> AddDishIngredientAsync(
            [FromRoute] Guid dishId,
            [FromBody] AddDishIngredientRequest request)
        {
            await dishService.AddDishIngredientAsync(dishId, request);

            return Created();
        }

        /// <summary>
        /// Removes an ingredient from the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="dishIngredientId">The dish ingredient identifier.</param>
        /// <returns>An <see cref="ActionResult"/> representing a no-content response.</returns>
        [HttpDelete("{dishId:guid}/ingredients/{dishIngredientId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<ActionResult> RemoveDishIngredientAsync(
            [FromRoute] Guid dishId,
            [FromRoute] Guid dishIngredientId)
        {
            await dishService.RemoveDishIngredientAsync(dishId, dishIngredientId);

            return NoContent();
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
