using Application.Abstractions.Services;
using Application.DTOs.Specifications;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing specifications.
    /// </summary>
    /// <param name="specificationService">The specification service.</param>
    [ApiController]
    [Route("[controller]")]
    public class SpecificationsController(ISpecificationService specificationService) : ControllerBase
    {
        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <param name="request">The specification creation request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost]
        [HasPermission(Permissions.Specifications.Create)]
        public async Task<ActionResult> CreateSpecificationAsync([FromBody] CreateSpecificationRequest request)
        {
            await specificationService.CreateSpecificationAsync(request);

            return Created();
        }

        /// <summary>
        /// Retrieves all specifications.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with all specifications.</returns>
        [HttpGet]
        [HasPermission(Permissions.Specifications.Read)]
        public async Task<ActionResult<IReadOnlyList<SpecificationResponse>>> GetSpecificationsAsync()
        {
            var specifications = await specificationService.GetSpecificationsAsync();

            return Ok(specifications);
        }

        /// <summary>
        /// Retrieves a specification by its identifier.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the matching specification.</returns>
        [HttpGet("{specificationId:guid}")]
        [HasPermission(Permissions.Specifications.Read)]
        public async Task<ActionResult<SpecificationResponse>> GetSpecificationById([FromRoute] Guid specificationId)
        {
            var specification = await specificationService.GetSpecificationByIdAsync(specificationId);

            return Ok(specification);
        }

        /// <summary>
        /// Updates an existing specification.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <param name="request">The specification update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("{specificationId:guid}")]
        [HasPermission(Permissions.Specifications.Update)]
        public async Task<ActionResult> UpdateSpecificationAsync(
            [FromRoute] Guid specificationId,
            [FromBody] UpdateSpecificationRequest request)
        {
            await specificationService.UpdateSpecificationAsync(specificationId, request);

            return Ok();
        }

        /// <summary>
        /// Removes a specification.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{specificationId:guid}")]
        [HasPermission(Permissions.Specifications.Delete)]
        public async Task<ActionResult<SpecificationResponse>> RemoveSpecificationAsync([FromRoute] Guid specificationId)
        {
            await specificationService.RemoveSpecificationAsync(specificationId);

            return NoContent();
        }
    }
}
