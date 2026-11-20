using Application.Abstractions.Services;
using Application.DTOs.Restaurants;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing restaurants.
    /// </summary>
    /// <param name="restaurantService">The restaurant service.</param>
    [ApiController]
    [Route("[controller]")]
    public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
    {
        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="request">The restaurant creation request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost]
        [HasPermission(Permissions.Restaurants.Create)]
        public async Task<ActionResult> CreateRestaurantAsync([FromBody] CreateRestaurantRequest request)
        {
            await restaurantService.CreateRestaurantAsync(request);

            return Created();
        }

        /// <summary>
        /// Retrieves all restaurants, optionally filtered by name.
        /// </summary>
        /// <param name="search">Optional substring to match against the restaurant name.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with all matching restaurants.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RestaurantResponse>>> GetRestaurantsAsync([FromQuery] string? search)
        {
            var restaurants = await restaurantService.GetRestaurantsAsync(search);

            return Ok(restaurants);
        }

        /// <summary>
        /// Retrieves the restaurant assigned to the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the current user restaurant.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<RestaurantResponse>> GetMyRestaurantAsync()
        {
            var restaurant = await restaurantService.GetUserRestaurantAsync(User.GetCurrentId());

            return Ok(restaurant);
        }

        /// <summary>
        /// Updates the restaurant assigned to the current user.
        /// </summary>
        /// <param name="request">The restaurant update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("me")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> UpdateMyRestaurantAsync([FromBody] UpdateRestaurantRequest request)
        {
            await restaurantService.UpdateUserRestaurantAsync(User.GetCurrentId(), request);

            return Ok();
        }

        /// <summary>
        /// Retrieves a restaurant by its identifier.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the matching restaurant.</returns>
        [HttpGet("{restaurantId:guid}")]
        public async Task<ActionResult<RestaurantResponse>> GetRestaurantByIdAsync([FromRoute] Guid restaurantId)
        {
            var restaurant = await restaurantService.GetRestaurantByIdAsync(restaurantId);

            return Ok(restaurant);
        }

        /// <summary>
        /// Updates an existing restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="request">The restaurant update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("{restaurantId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> UpdateRestaurantAsync(
            [FromRoute] Guid restaurantId,
            [FromBody] UpdateRestaurantRequest request)
        {
            await restaurantService.UpdateRestaurantAsync(restaurantId, request);

            return Ok();
        }

        /// <summary>
        /// Adds a user to a restaurant.
        /// </summary>
        /// <param name="request">The restaurant user addition request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("users")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddRestaurantUserAsync([FromBody] AddRestaurantUserRequest request)
        {
            await restaurantService.AddRestaurantUserAsync(request.RestaurantId, request);

            return Created();
        }

        /// <summary>
        /// Removes a user from a restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{restaurantId:guid}/users/{userId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> RemoveRestaurantUserAsync(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid userId)
        {
            await restaurantService.RemoveRestaurantUserAsync(restaurantId, userId);

            return NoContent();
        }

        /// <summary>
        /// Adds an address to the restaurant assigned to the current user.
        /// </summary>
        /// <param name="request">The restaurant address request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("me/addresses")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddMyRestaurantAddressAsync([FromBody] UpdateRestaurantAddressRequest request)
        {
            await restaurantService.AddUserRestaurantAddressAsync(User.GetCurrentId(), request);

            return Created();
        }

        /// <summary>
        /// Updates an address of the restaurant assigned to the current user.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="request">The restaurant address update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("me/addresses/{addressId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> UpdateMyRestaurantAddressAsync(
            [FromRoute] Guid addressId,
            [FromBody] UpdateRestaurantAddressRequest request)
        {
            await restaurantService.UpdateUserRestaurantAddressAsync(User.GetCurrentId(), addressId, request);

            return Ok();
        }

        /// <summary>
        /// Removes an address from the restaurant assigned to the current user.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("me/addresses/{addressId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> RemoveMyRestaurantAddressAsync([FromRoute] Guid addressId)
        {
            await restaurantService.RemoveUserRestaurantAddressAsync(User.GetCurrentId(), addressId);

            return NoContent();
        }

        /// <summary>
        /// Adds an ingredient to the restaurant assigned to the current user.
        /// </summary>
        /// <param name="request">The restaurant ingredient request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("me/ingredients")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddMyRestaurantIngredientAsync([FromBody] AddMyRestaurantIngredientRequest request)
        {
            await restaurantService.AddUserRestaurantIngredientAsync(User.GetCurrentId(), request);

            return Created();
        }

        /// <summary>
        /// Updates an ingredient of the restaurant assigned to the current user.
        /// </summary>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <param name="request">The restaurant ingredient update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("me/ingredients/{ingredientId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> UpdateMyRestaurantIngredientAsync(
            [FromRoute] Guid ingredientId,
            [FromBody] UpdateRestaurantIngredientRequest request)
        {
            await restaurantService.UpdateUserRestaurantIngredientAsync(User.GetCurrentId(), ingredientId, request);

            return Ok();
        }

        /// <summary>
        /// Removes an ingredient from the restaurant assigned to the current user.
        /// </summary>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("me/ingredients/{ingredientId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> RemoveMyRestaurantIngredientAsync([FromRoute] Guid ingredientId)
        {
            await restaurantService.RemoveUserRestaurantIngredientAsync(User.GetCurrentId(), ingredientId);

            return NoContent();
        }

        /// <summary>
        /// Adds an address to a restaurant.
        /// </summary>
        /// <param name="request">The restaurant address addition request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("addresses")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddRestaurantAddressAsync([FromBody] AddRestaurantAddressRequest request)
        {
            await restaurantService.AddRestaurantAddressAsync(request.RestaurantId, request);

            return Created();
        }

        /// <summary>
        /// Removes an address from a restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{restaurantId:guid}/addresses/{addressId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> RemoveRestaurantAddressAsync(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid addressId)
        {
            await restaurantService.RemoveRestaurantAddressAsync(restaurantId, addressId);

            return NoContent();
        }

        /// <summary>
        /// Updates a restaurant address.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="request">The restaurant address update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("{restaurantId:guid}/addresses/{addressId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> UpdateRestaurantAddressAsync(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid addressId,
            [FromBody] UpdateRestaurantAddressRequest request)
        {
            await restaurantService.UpdateRestaurantAddressAsync(restaurantId, addressId, request);

            return Ok();
        }

        /// <summary>
        /// Adds an ingredient to a restaurant.
        /// </summary>
        /// <param name="request">The restaurant ingredient addition request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("ingredients")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddRestaurantIngredientAsync([FromBody] AddRestaurantIngredientRequest request)
        {
            await restaurantService.AddRestaurantIngredientAsync(request.RestaurantId, request);

            return Created();
        }

        /// <summary>
        /// Updates a restaurant ingredient.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <param name="request">The restaurant ingredient update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("{restaurantId:guid}/ingredients/{ingredientId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> UpdateRestaurantIngredientAsync(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid ingredientId,
            [FromBody] UpdateRestaurantIngredientRequest request)
        {
            await restaurantService.UpdateRestaurantIngredientAsync(restaurantId, ingredientId, request);

            return Ok();
        }

        /// <summary>
        /// Removes an ingredient from a restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{restaurantId:guid}/ingredients/{ingredientId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> RemoveRestaurantIngredientAsync(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid ingredientId)
        {
            await restaurantService.RemoveRestaurantIngredientAsync(restaurantId, ingredientId);

            return NoContent();
        }
    }
}
