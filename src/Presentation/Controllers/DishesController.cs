using Application.Abstractions.Services;
using Application.DTOs.Dishes;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishesController(IDishService dishService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Dishes.Create)]
        public async Task<ActionResult> CreateDishAsync([FromBody] CreateDishRequest request)
        {
            await dishService.CreateDishAsync(request);

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DishResponse>>> GetDishesAsync()
        {
            var dishes = await dishService.GetDishesAsync();

            return Ok(dishes);
        }

        [HttpGet("{dishId:guid}")]
        public async Task<ActionResult<DishResponse>> GetDishById([FromRoute] Guid dishId)
        {
            var dish = await dishService.GetDishByIdAsync(dishId);

            return Ok(dish);
        }

        [HttpPut("{dishId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<ActionResult> UpdateDishAsync(
            [FromRoute] Guid dishId,
            [FromBody] UpdateDishRequest request)
        {
            await dishService.UpdateDishAsync(dishId, request);

            return Ok();
        }

        [HttpDelete("{dishId:guid}")]
        [HasPermission(Permissions.Dishes.Delete)]
        public async Task<ActionResult> RemoveDishAsync([FromRoute] Guid dishId)
        {
            await dishService.RemoveDishAsync(dishId);

            return NoContent();
        }
    }
}
