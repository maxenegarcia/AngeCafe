
using AngeCafe.API.Models;
using AngeCafe.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AngeCafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return BadRequest("Category name is required.");
            }

            category.CategoryName = category.CategoryName.Trim();

            await _categoryService.AddAsync(category);

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.CategoryId },
                category
            );
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(
            int id, Category updatedCategory)
        {
            if (string.IsNullOrWhiteSpace(updatedCategory.CategoryName))
            {
                return BadRequest("Category name is required.");
            }

            updatedCategory.CategoryName =
                updatedCategory.CategoryName.Trim();

            updatedCategory.CategoryId = id;

            try
            {
                await _categoryService.UpdateAsync(updatedCategory);

                return Ok(await _categoryService.GetByIdAsync(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found.");
            }
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                bool deleted = await _categoryService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound("Category not found.");
                }

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest(
                    "Cannot delete a category that contains menu items."
                );
            }
        }
    }
}
