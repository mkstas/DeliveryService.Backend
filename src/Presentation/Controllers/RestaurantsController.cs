using Application.Abstractions.Services;
using Application.DTOs.Restaurants;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

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
        /// Retrieves all restaurants.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with all restaurants.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RestaurantResponse>>> GetRestaurantsAsync()
        {
            var restaurants = await restaurantService.GetRestaurantsAsync();

            return Ok(restaurants);
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
