using DeliveryService.Domain.Common;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Categories;
using DeliveryService.Web.Contracts.Dishes;
using DeliveryService.Web.Contracts.Ingredients;
using DeliveryService.Web.Contracts.Modifiers;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishesController(IDishService dishService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Dishes.Create)]
        public async Task<IActionResult> CreateDishAsync([FromBody] CreateDishRequest request)
        {
            await dishService.CreateDishAsync(request.EstablishmentId, request.CategoryId, StringBounded.Create(request.Name),
                request.Description is null ? null : StringUnbounded.Create(request.Description));

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetDishesAsync()
        {
            var dishes = await dishService.GetDishesAsync();

            List<DishResponse> response = [.. dishes.Select(d => PrepareDishResponse(d))];

            return Ok(response);
        }

        [HttpGet("{dishId:guid}")]
        public async Task<IActionResult> GetDishByIdAsync([FromRoute] Guid dishId)
        {
            var dish = await dishService.GetDishByIdAsync(dishId);
            var response = PrepareDishResponse(dish);

            return Ok(response);
        }

        private static DishResponse PrepareDishResponse(Dish dish)
        {
            return new DishResponse(
                dish.Id,
                dish.Name,
                dish.Description is null ? null : (string)dish.Description,
                new DishEstablishmentResponse(dish.Establishment.Id, dish.Establishment.Name),
                new CategoryResponse(dish.Category.Id, dish.Category.Name),
                [.. dish.Ingredients.Select(i => new DishIngredientResponse(i.Id, i.Name))],
                [.. dish.Modifiers.Select(m => new ModifierResponse(m.Id, m.Name, m.Price, m.ImageUrl is null ? null : (string)m.ImageUrl))],
                [..dish.Variants.Select(v => new DishVariantResponse(v.Id, v.Price, v.ImageUrl is null ? null : (string)v.ImageUrl,
                    [.. v.Specifications.Select(s => new DishSpecificationResponse(s.Id, s.Specification.Name, s.Value))]))]
            );
        }

        [HttpPut("{dishId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> UpdateDishAsync(
            [FromRoute] Guid dishId,
            [FromBody] UpdateDishRequest request)
        {
            await dishService.UpdateDishAsync(dishId, request.CategoryId, StringBounded.Create(request.Name),
                request.Description is null ? null : StringUnbounded.Create(request.Description));

            return NoContent();
        }

        [HttpPost("ingredients")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> AddDishIngredientAsync([FromBody] AddDishIngredientRequest request)
        {
            await dishService.AddIngredientAsync(request.DishId, request.IngredientId);

            return NoContent();
        }

        [HttpDelete("{dishId:guid}/ingredients/{ingredientId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> RemoveDishIngredientAsync(
            [FromRoute] Guid dishId,
            [FromRoute] Guid ingredientId)
        {
            await dishService.RemoveIngredientAsync(dishId, ingredientId);

            return NoContent();
        }

        [HttpPost("modifiers")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> AddDishModifierAsync([FromBody] AddDishModifierRequest request)
        {
            await dishService.AddModifierAsync(request.DishId, request.ModifierId);

            return NoContent();
        }

        [HttpDelete("{dishId:guid}/modifiers/{modifierId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> RemoveDishModifierAsync(
            [FromRoute] Guid dishId,
            [FromRoute] Guid modifierId)
        {
            await dishService.RemoveModifierAsync(dishId, modifierId);

            return NoContent();
        }

        [HttpPost("variants")]
        [HasPermission(Permissions.Dishes.Create)]
        public async Task<IActionResult> CreateDishVariantAsync([FromBody] CreateDishVariantRequest request)
        {
            await dishService.CreateVariantAsync(request.DishId, Currency.Create(request.Price));

            return NoContent();
        }

        [HttpPatch("{dishId:guid}/variants/{variantId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> UpdateDishVariantAsync(
            [FromRoute] Guid dishId,
            [FromRoute] Guid variantId,
            [FromBody] UpdateDishVariantRequest request)
        {
            await dishService.UpdateVariantAsync(dishId, variantId, Currency.Create(request.Price));

            return NoContent();
        }

        [HttpPost("variants/specifications")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> AddDishVariantSpecificationAsync([FromBody] AddDishVariantSpecificationRequest request)
        {
            await dishService.AddVariantSpecificationAsync(
                request.DishId, request.DishVariantId, request.SpecificationId, StringBounded.Create(request.Value));

            return NoContent();
        }

        [HttpDelete("{dishId:guid}/variants/{variantId:guid}/specifications/{specificationId:guid}")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> RemoveDishVariantSpecificationAsync(
            [FromRoute] Guid dishId,
            [FromRoute] Guid variantId,
            [FromRoute] Guid specificationId)
        {
            await dishService.RemoveVariantSpecificationAsync(dishId, variantId, specificationId);

            return NoContent();
        }
    }
}
