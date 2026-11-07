using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Categories.Create)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            await categoryService.CreateCategoryAsync(StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await categoryService.GetCategoriesAsync();

            List<CategoryResponse> response = [.. categories.Select(c => new CategoryResponse(c.Id, c.Name))];

            return Ok(response);
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid categoryId)
        {
            var category = await categoryService.GetCategoryByIdAsync(categoryId);

            var response = new CategoryResponse(category.Id, category.Name);

            return Ok(response);
        }

        [HttpPut("{categoryId:guid}")]
        [HasPermission(Permissions.Categories.Update)]
        public async Task<IActionResult> UpdateCategoryAsync(
            [FromRoute] Guid categoryId,
            [FromBody] UpdateCategoryRequest request)
        {
            await categoryService.UpdateCategoryAsync(categoryId, StringBounded.Create(request.Name));

            return NoContent();
        }
    }
}
