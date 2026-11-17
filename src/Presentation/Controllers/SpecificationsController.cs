using Application.Abstractions.Services;
using Application.DTOs.Specifications;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecificationsController(ISpecificationService specificationService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Specifications.Create)]
        public async Task<ActionResult> CreateSpecificationAsync([FromBody] CreateSpecificationRequest request)
        {
            await specificationService.CreateSpecificationAsync(request);

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SpecificationResponse>>> GetSpecificationsAsync()
        {
            var specifications = await specificationService.GetSpecificationsAsync();

            return Ok(specifications);
        }

        [HttpGet("{specificationId:guid}")]
        [HasPermission(Permissions.Specifications.Read)]
        public async Task<ActionResult<SpecificationResponse>> GetSpecificationById([FromRoute] Guid specificationId)
        {
            var specification = await specificationService.GetSpecificationByIdAsync(specificationId);

            return Ok(specification);
        }

        [HttpPut("{specificationId:guid}")]
        [HasPermission(Permissions.Specifications.Update)]
        public async Task<ActionResult> UpdateSpecificationAsync(
            [FromRoute] Guid specificationId,
            [FromBody] UpdateSpecificationRequest request)
        {
            await specificationService.UpdateSpecificationAsync(specificationId, request);

            return Ok();
        }

        [HttpDelete("{specificationId:guid}")]
        [HasPermission(Permissions.Specifications.Delete)]
        public async Task<ActionResult<SpecificationResponse>> RemoveSpecificationAsync([FromRoute] Guid specificationId)
        {
            await specificationService.RemoveSpecificationAsync(specificationId);

            return NoContent();
        }
    }
}
