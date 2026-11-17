using Application.Abstractions.Services;
using Application.DTOs.DishSpecifications;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing dish specifications.
    /// </summary>
    /// <param name="dishSpecificationService">The dish specification service.</param>
    [ApiController]
    [Route("[controller]")]
    public class DishSpecificationsController(IDishSpecificationService dishSpecificationService) : ControllerBase
    {
        /// <summary>
        /// Creates a new dish specification.
        /// </summary>
        /// <param name="request">The request to create dish specification.</param>
        /// <returns>An <see cref="ActionResult"/> representing a created response.</returns>
        [HttpPost]
        [HasPermission(Permissions.DishSpecifications.Create)]
        public async Task<ActionResult> CreateDishSpecificationAsync([FromBody] CreateDishSpecificationRequest request)
        {
            await dishSpecificationService.CreateDishSpecificationAsync(request);

            return Created();
        }

        /// <summary>
        /// Returns all dish specifications in the system.
        /// </summary>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the dish specifications.</returns>
        [HttpGet]
        [HasPermission(Permissions.DishSpecifications.Read)]
        public async Task<ActionResult<IReadOnlyList<DishSpecificationResponse>>> GetDishSpecificationsAsync()
        {
            var specifications = await dishSpecificationService.GetDishSpecificationsAsync();

            return Ok(specifications);
        }

        /// <summary>
        /// Returns a dish specification by its identifier.
        /// </summary>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the dish specification.</returns>
        [HttpGet("{dishSpecificationId:guid}")]
        [HasPermission(Permissions.DishSpecifications.Read)]
        public async Task<ActionResult<DishSpecificationResponse>> GetDishSpecificationById([FromRoute] Guid dishSpecificationId)
        {
            var specification = await dishSpecificationService.GetDishSpecificationByIdAsync(dishSpecificationId);

            return Ok(specification);
        }

        /// <summary>
        /// Updates the specified dish specification.
        /// </summary>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <param name="request">The request to update dish specification.</param>
        /// <returns>An <see cref="ActionResult"/> representing an OK response.</returns>
        [HttpPut("{dishSpecificationId:guid}")]
        [HasPermission(Permissions.DishSpecifications.Update)]
        public async Task<ActionResult> UpdateDishSpecificationAsync(
            [FromRoute] Guid dishSpecificationId,
            [FromBody] UpdateDishSpecificationRequest request)
        {
            await dishSpecificationService.UpdateDishSpecificationAsync(dishSpecificationId, request);

            return Ok();
        }

        /// <summary>
        /// Removes the specified dish specification.
        /// </summary>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <returns>An <see cref="ActionResult"/> representing a no-content response.</returns>
        [HttpDelete("{dishSpecificationId:guid}")]
        [HasPermission(Permissions.DishSpecifications.Delete)]
        public async Task<ActionResult> RemoveDishSpecificationAsync([FromRoute] Guid dishSpecificationId)
        {
            await dishSpecificationService.RemoveDishSpecificationAsync(dishSpecificationId);

            return NoContent();
        }
    }
}
