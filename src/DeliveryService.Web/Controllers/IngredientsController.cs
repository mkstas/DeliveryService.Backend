using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController(IIngredientService ingredientService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Ingredients.Create)]
        public async Task<IActionResult> CreateIngredientAsync([FromBody] CreateIngredientRequest request)
        {
            await ingredientService.CreateIngredientAsync(request.EstablishmentId, StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpGet]
        [HasPermission(Permissions.Ingredients.Read)]
        public async Task<IActionResult> GetIngredientsAsync()
        {
            var ingredients = await ingredientService.GetIngredientsAsync();

            List<IngredientResponse> response = [.. ingredients.Select(i => new IngredientResponse(
                i.Id, i.Name, new IngredientEstablishmentResponse(i.Establishment.Id, i.Establishment.Name)))];

            return Ok(response);
        }

        [HttpGet("{establishmentId:guid}")]
        [HasPermission(Permissions.Ingredients.Read)]
        public async Task<IActionResult> GetIngredientsByEstablishmentIdAsync([FromRoute] Guid establishmentId)
        {
            var ingredients = await ingredientService.GetIngredientsByEstablishmentIdAsync(establishmentId);

            List<IngredientResponse> response = [.. ingredients.Select(i => new IngredientResponse(
                i.Id, i.Name, new IngredientEstablishmentResponse(i.Establishment.Id, i.Establishment.Name)))];

            return Ok(response);
        }

        [HttpPut("{ingredientId:guid}")]
        [HasPermission(Permissions.Ingredients.Update)]
        public async Task<IActionResult> UpdateIngredientAsync(
            [FromRoute] Guid ingredientId,
            [FromBody] CreateIngredientRequest request)
        {
            await ingredientService.UpdateIngredientAsync(ingredientId, StringBounded.Create(request.Name));

            return NoContent();
        }
    }
}
