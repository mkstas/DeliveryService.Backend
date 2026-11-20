using Application.Abstractions.Services;
using Application.Common;
using Application.DTOs.Categories;
using Application.DTOs.Common;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing categories.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="request">The request to create category.</param>
        /// <returns>An <see cref="ActionResult"/> representing a created response.</returns>
        [HttpPost]
        [HasPermission(Permissions.Categories.Create)]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            await categoryService.CreateCategoryAsync(request);

            return Created();
        }

        /// <summary>
        /// Returns all categories in the system, optionally filtered by name.
        /// </summary>
        /// <param name="search">Optional substring to match against the category name.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> GetCategoriesAsync([FromQuery] string? search)
        {
            var categories = await categoryService.GetCategoriesAsync(search);

            return Ok(categories);
        }

        [HttpGet("page")]
        public async Task<ActionResult<PagedResponse<CategoryResponse>>> GetCategoriesPageAsync(
            [FromQuery] string? search,
            [FromQuery] PaginationQuery pagination)
        {
            var categories = await categoryService.GetCategoriesAsync(search);

            return Ok(categories.ToPaged(pagination));
        }

        /// <summary>
        /// Returns a category by its identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> containing the category.</returns>
        [HttpGet("{categoryId:guid}")]
        [HasPermission(Permissions.Categories.Read)]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById([FromRoute] Guid categoryId)
        {
            var category = await categoryService.GetCategoryByIdAsync(categoryId);

            return Ok(category);
        }

        /// <summary>
        /// Updates the specified category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="request">The request to update category.</param>
        /// <returns>An <see cref="ActionResult"/> representing an OK response.</returns>
        [HttpPut("{categoryId:guid}")]
        [HasPermission(Permissions.Categories.Update)]
        public async Task<ActionResult> UpdateCategoryAsync(
            [FromRoute] Guid categoryId,
            [FromBody] UpdateCategoryRequest request)
        {
            await categoryService.UpdateCategoryAsync(categoryId, request);

            return Ok();
        }

        /// <summary>
        /// Removes the specified category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>An <see cref="ActionResult{TValue}"/> representing a no-content response.</returns>
        [HttpDelete("{categoryId:guid}")]
        [HasPermission(Permissions.Categories.Delete)]
        public async Task<ActionResult<CategoryResponse>> RemoveCategoryAsync([FromRoute] Guid categoryId)
        {
            await categoryService.RemoveCategoryAsync(categoryId);

            return NoContent();
        }
    }
}
