using AngeCafe.API.Data;
using AngeCafe.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(category);
        }

     
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return BadRequest("Category name is required.");
            }

            category.CategoryName = category.CategoryName.Trim();

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.CategoryId },
                category
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(
            int id, Category updatedCategory)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedCategory.CategoryName))
            {
                return BadRequest("Category name is required.");
            }

            category.CategoryName = updatedCategory.CategoryName.Trim();

            await _context.SaveChangesAsync();

            return Ok(category);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }

            bool hasMenuItems = await _context.MenuItems
                .AnyAsync(m => m.CategoryId == id);

            if (hasMenuItems)
            {
                return BadRequest(
                    "Cannot delete a category that contains menu items."
                );
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}