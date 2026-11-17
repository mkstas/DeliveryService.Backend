using Application.Abstractions.Services;
using Application.DTOs.Restaurants;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Restaurants.Create)]
        public async Task<ActionResult> CreateRestaurantAsync([FromBody] CreateRestaurantRequest request)
        {
            await restaurantService.CreateRestaurantAsync(request);

            return Created();
        }

        [HttpGet]
        [HasPermission(Permissions.Restaurants.Read)]
        public async Task<ActionResult<IReadOnlyList<RestaurantResponse>>> GetRestaurantsAsync()
        {
            var restaurants = await restaurantService.GetRestaurantsAsync();

            return Ok(restaurants);
        }

        [HttpGet("{restaurantId:guid}")]
        [HasPermission(Permissions.Restaurants.Read)]
        public async Task<ActionResult<RestaurantResponse>> GetRestaurantByIdAsync([FromRoute] Guid restaurantId)
        {
            var restaurant = await restaurantService.GetRestaurantByIdAsync(restaurantId);

            return Ok(restaurant);
        }

        [HttpPut("{restaurantId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> UpdateRestaurantAsync(
            [FromRoute] Guid restaurantId,
            [FromBody] UpdateRestaurantRequest request)
        {
            await restaurantService.UpdateRestaurantAsync(restaurantId, request);

            return Ok();
        }

        [HttpPost("users")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddRestaurantUserAsync([FromBody] AddRestaurantUserRequest request)
        {
            await restaurantService.AddRestaurantUserAsync(request.RestaurantId, request);

            return Created();
        }

        [HttpDelete("{restaurantId:guid}/users/{userId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> RemoveRestaurantUserAsync(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid userId)
        {
            await restaurantService.RemoveRestaurantUserAsync(restaurantId, userId);

            return NoContent();
        }

        [HttpPost("addresses")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddRestaurantAddressAsync([FromBody] AddRestaurantAddressRequest request)
        {
            await restaurantService.AddRestaurantAddressAsync(request.RestaurantId, request);

            return Created();
        }

        [HttpDelete("{restaurantId:guid}/addresses/{addressId:guid}")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> RemoveRestaurantAddressAsync(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid addressId)
        {
            await restaurantService.RemoveRestaurantAddressAsync(restaurantId, addressId);

            return NoContent();
        }

        [HttpPost("ingredients")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<ActionResult> AddRestaurantIngredientAsync([FromBody] AddRestaurantIngredientRequest request)
        {
            await restaurantService.AddRestaurantIngredientAsync(request.RestaurantId, request);

            return Created();
        }

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
    }
}
