using Application.Abstractions.Services;
using Application.DTOs.Categories;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Categories.Create)]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            await categoryService.CreateCategoryAsync(request);

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> GetCategoriesAsync()
        {
            var categories = await categoryService.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("{categoryId:guid}")]
        [HasPermission(Permissions.Categories.Read)]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById([FromRoute] Guid categoryId)
        {
            var category = await categoryService.GetCategoryByIdAsync(categoryId);

            return Ok(category);
        }

        [HttpPut("{categoryId:guid}")]
        [HasPermission(Permissions.Categories.Update)]
        public async Task<ActionResult> UpdateCategoryAsync(
            [FromRoute] Guid categoryId,
            [FromBody] UpdateCategoryRequest request)
        {
            await categoryService.UpdateCategoryAsync(categoryId, request);

            return Ok();
        }

        [HttpDelete("{categoryId:guid}")]
        [HasPermission(Permissions.Categories.Delete)]
        public async Task<ActionResult<CategoryResponse>> RemoveCategoryAsync([FromRoute] Guid categoryId)
        {
            await categoryService.RemoveCategoryAsync(categoryId);

            return NoContent();
        }
    }
}
