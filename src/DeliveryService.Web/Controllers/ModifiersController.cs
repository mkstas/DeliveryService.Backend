using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Modifiers;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModifiersController(IModifierService modifierService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Modifiers.Create)]
        public async Task<IActionResult> CreateModifierAsync([FromBody] CreateModifierRequest request)
        {
            await modifierService.CreateModifierAsync(
                request.EstablishmentId, StringBounded.Create(request.Name), Currency.Create(request.Price));

            return NoContent();
        }

        [HttpPut("{modifierId:guid}")]
        [HasPermission(Permissions.Modifiers.Update)]
        public async Task<IActionResult> UpdateModifierAsync(
            [FromRoute] Guid modifierId,
            [FromBody] UpdateModifierRequest request)
        {
            await modifierService.UpdateModifierAsync(modifierId, StringBounded.Create(request.Name), Currency.Create(request.Price));

            return NoContent();
        }
    }
}
