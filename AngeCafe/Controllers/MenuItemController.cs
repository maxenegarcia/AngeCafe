
using AngeCafe.API.Data;
using AngeCafe.API.DTOs;
using AngeCafe.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuItemController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemResponseDto>>> GetMenuItems()
        {
            var items = await _context.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Variants)
                .ToListAsync();

            return Ok(items.Select(MapToResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemResponseDto>> GetMenuItem(int id)
        {
            var item = await _context.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Variants)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);

            if (item == null)
                return NotFound();

            return Ok(MapToResponse(item));
        }

        
        [HttpPost]
        public async Task<ActionResult<MenuItemResponseDto>> CreateMenuItem(
            MenuItemRequestDto request)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.CategoryId == request.CategoryId);

            if (!categoryExists)
                return BadRequest("The selected category does not exist.");

            if (request.Variants == null || !request.Variants.Any())
                return BadRequest("A menu item must have at least one variant.");

            var item = new MenuItem
            {
                CategoryId = request.CategoryId,
                ItemName = request.ItemName,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                Variants = request.Variants.Select(v => new MenuItemVariant
                {
                    VariantName = v.VariantName,
                    Price = v.Price,
                    IsAvailable = v.IsAvailable
                }).ToList()
            };

            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();

            var createdItem = await _context.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Variants)
                .FirstAsync(m => m.MenuItemId == item.MenuItemId);

            return CreatedAtAction(
                nameof(GetMenuItem),
                new { id = item.MenuItemId },
                MapToResponse(createdItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(
            int id, MenuItemRequestDto request)
        {
            var item = await _context.MenuItems
                .Include(m => m.Variants)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);

            if (item == null)
                return NotFound();

            var categoryExists = await _context.Categories
                .AnyAsync(c => c.CategoryId == request.CategoryId);

            if (!categoryExists)
                return BadRequest("The selected category does not exist.");

            if (request.Variants == null || !request.Variants.Any())
                return BadRequest("A menu item must have at least one variant.");

            item.CategoryId = request.CategoryId;
            item.ItemName = request.ItemName;
            item.Description = request.Description;
            item.ImageUrl = request.ImageUrl;
            item.IsActive = request.IsActive;

            _context.MenuItemVariants.RemoveRange(item.Variants);

            item.Variants = request.Variants.Select(v => new MenuItemVariant
            {
                VariantName = v.VariantName,
                Price = v.Price,
                IsAvailable = v.IsAvailable
            }).ToList();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateMenuItem(int id)
        {
            var item = await _context.MenuItems
                .FirstOrDefaultAsync(m => m.MenuItemId == id);

            if (item == null)
                return NotFound();

            item.IsActive = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static MenuItemResponseDto MapToResponse(MenuItem item)
        {
            return new MenuItemResponseDto
            {
                MenuItemId = item.MenuItemId,
                CategoryId = item.CategoryId,
                CategoryName = item.Category?.CategoryName ?? string.Empty,
                ItemName = item.ItemName,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                IsActive = item.IsActive,
                Variants = item.Variants.Select(v => new MenuItemVariantResponseDto
                {
                    VariantId = v.VariantId,
                    VariantName = v.VariantName,
                    Price = v.Price,
                    IsAvailable = v.IsAvailable
                }).ToList()
            };
        }
    }
}
