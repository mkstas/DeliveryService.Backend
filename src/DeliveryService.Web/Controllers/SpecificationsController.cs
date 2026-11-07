using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecificationsController(ISpecificationService specificationService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Specifications.Create)]
        public async Task<IActionResult> CreateSpecificationAsync([FromBody] CreateSpecificationRequest request)
        {
            await specificationService.CreateSpecificationAsync(StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecificationsAsync()
        {
            var specifications = await specificationService.GetSpecificationsAsync();

            List<SpecificationResponse> response = [.. specifications.Select(s => new SpecificationResponse(s.Id, s.Name))];

            return Ok(response);
        }

        [HttpGet("{specificationId:guid}")]
        public async Task<IActionResult> GetSpecificationByIdAsync([FromRoute] Guid specificationId)
        {
            var specification = await specificationService.GetSpecificationByIdAsync(specificationId);

            var response = new SpecificationResponse(specification.Id, specification.Name);

            return Ok(specification);
        }

        [HttpPut("{specificationId:guid}")]
        public async Task<IActionResult> UpdateSpecificationAsync(
            [FromRoute] Guid specificationId,
            [FromBody] UpdateSpecificationRequest request)
        {
            await specificationService.UpdateCpecificationAsync(specificationId, StringBounded.Create(request.Name));

            return NoContent();
        }
    }
}
