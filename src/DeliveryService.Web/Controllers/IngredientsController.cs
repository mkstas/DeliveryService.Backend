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
        [HasPermission(Permissions.Establishment.Update)]
        public async Task<IActionResult> CreateIngredientAsync([FromBody] CreateIngredientRequest request)
        {
            await ingredientService.CreateIngredientAsync(request.EstablishmentId, StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpPut("{ingredientId:guid}")]
        [HasPermission(Permissions.Establishment.Update)]
        public async Task<IActionResult> UpdateIngredientAsync(
            [FromRoute] Guid ingredientId,
            [FromBody] CreateIngredientRequest request)
        {
            await ingredientService.UpdateIngredientAsync(ingredientId, StringBounded.Create(request.Name));

            return NoContent();
        }
    }
}
