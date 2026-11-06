using AngeCafe.API.DTOs;
using AngeCafe.API.Models;
using AngeCafe.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AngeCafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET: api/MenuItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemResponseDto>>>
            GetMenuItems()
        {
            var items = await _menuItemService.GetAllAsync();

            return Ok(items.Select(MapToResponse));
        }

        // GET: api/MenuItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemResponseDto>>
            GetMenuItem(int id)
        {
            var item = await _menuItemService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound("Menu item not found.");
            }

            return Ok(MapToResponse(item));
        }

        // POST: api/MenuItem
        [HttpPost]
        public async Task<ActionResult<MenuItemResponseDto>>
            CreateMenuItem(MenuItemRequestDto request)
        {
            var item = new MenuItem
            {
                CategoryId = request.CategoryId,
                ItemName = request.ItemName,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,

                Variants = request.Variants?.Select(v =>
                    new MenuItemVariant
                    {
                        VariantName = v.VariantName,
                        Price = v.Price,
                        IsAvailable = v.IsAvailable
                    }).ToList() ?? new List<MenuItemVariant>()
            };

            try
            {
                await _menuItemService.AddAsync(item);

                var createdItem =
                    await _menuItemService.GetByIdAsync(item.MenuItemId);

                if (createdItem == null)
                {
                    return StatusCode(500,
                        "Menu item was created but could not be retrieved.");
                }

                return CreatedAtAction(
                    nameof(GetMenuItem),
                    new { id = item.MenuItemId },
                    MapToResponse(createdItem));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/MenuItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(
            int id, MenuItemRequestDto request)
        {
            var item = new MenuItem
            {
                MenuItemId = id,
                CategoryId = request.CategoryId,
                ItemName = request.ItemName,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,

                Variants = request.Variants?.Select(v =>
                    new MenuItemVariant
                    {
                        VariantName = v.VariantName,
                        Price = v.Price,
                        IsAvailable = v.IsAvailable
                    }).ToList() ?? new List<MenuItemVariant>()
            };

            try
            {
                await _menuItemService.UpdateAsync(item);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Menu item not found.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/MenuItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateMenuItem(int id)
        {
            bool deleted = await _menuItemService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound("Menu item not found.");
            }

            return NoContent();
        }

        // Mapping
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

                Variants = item.Variants.Select(v =>
                    new MenuItemVariantResponseDto
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
