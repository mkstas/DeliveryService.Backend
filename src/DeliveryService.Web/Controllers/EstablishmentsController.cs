using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Establishment;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstablishmentsController(IEstablishmentService establishmentService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Establishment.Create)]
        public async Task<IActionResult> CreateEstablishmentAsync([FromBody] CreateEstablishmentRequest request)
        {
            await establishmentService.CreateEstablishmentAsync(StringBounded.Create(request.Name), BankAccount.Create(request.BusinessAccount));

            return NoContent();
        }

        [HttpGet]
        [HasPermission(Permissions.Establishment.Read)]
        public async Task<IActionResult> GetEstablishmentsAsync()
        {
            var establishments = await establishmentService.GetEstablishmentsAsync();

            List<EstablishmentResponse> response = [.. establishments.Select(
                e => new EstablishmentResponse(e.Id, e.Name, e.BusinessAccount,
                [.. e.Addresses.Select(a => new EstablishmentAddressResponse(a.Id, a.Address))]))];

            return Ok(response);
        }

        [HttpGet("{establishmentId:guid}")]
        [HasPermission(Permissions.Establishment.Read)]
        public async Task<IActionResult> GetEstablishmentByIdAsync([FromRoute] Guid establishmentId)
        {
            var establishment = await establishmentService.GetEstablishmentByIdAsync(establishmentId);

            var response = new EstablishmentResponse(establishment.Id, establishment.Name, establishment.BusinessAccount,
                [.. establishment.Addresses.Select(a => new EstablishmentAddressResponse(a.Id, a.Address))]);

            return Ok(response);
        }

        [HttpPut("{establishmentId:guid}")]
        [HasPermission(Permissions.Establishment.Update)]
        public async Task<IActionResult> UpdateEstablishmentAsync(
            [FromRoute] Guid establishmentId,
            [FromBody] UpdateEstablishmentRequest request)
        {
            await establishmentService.UpdateEstablishmentAsync(
                establishmentId, StringBounded.Create(request.Name), BankAccount.Create(request.BusinessAccount));

            return NoContent();
        }

        [HttpPost("users")]
        [HasPermission(Permissions.Establishment.Update)]
        public async Task<IActionResult> AddUserEstablishmentAsync([FromBody] AddUserEstablishmentRequest request)
        {
            await establishmentService.AddUserEstablishmentAsync(request.EstablishmentId, EmailAddress.Create(request.EmailAddress));

            return NoContent();
        }

        [HttpDelete("{establishmentId:guid}/users/{userId:guid}")]
        [HasPermission(Permissions.Establishment.Update)]
        public async Task<IActionResult> RemoveUserEstablishmentAsync(
            [FromRoute] Guid establishmentId,
            [FromRoute] Guid userId)
        {
            await establishmentService.RemoveUserEstablishmentAsync(establishmentId, userId);

            return NoContent();
        }

        [HttpPost("addresses")]
        [HasPermission(Permissions.Establishment.Create)]
        public async Task<IActionResult> AddEstablishmentAddressAsync([FromBody] AddEstablishmentAddressRequest request)
        {
            await establishmentService.AddEstablishmentAddressAsync(request.EstablishmentId, StringBounded.Create(request.Address));

            return NoContent();
        }

        [HttpDelete("{establishmentId:guid}/addresses/{addressId:guid}")]
        [HasPermission(Permissions.Establishment.Update)]
        public async Task<IActionResult> RemoveEstablishmentAddressAsync(
            [FromRoute] Guid establishmentId,
            [FromRoute] Guid addressId)
        {
            await establishmentService.RemoveUserEstablishmentAsync(establishmentId, addressId);

            return NoContent();
        }
    }
}
