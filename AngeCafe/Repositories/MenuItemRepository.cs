using AngeCafe.API.Data;
using AngeCafe.API.Models;
using AngeCafe.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.API.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Variants)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _context.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Variants)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories
                .AnyAsync(c => c.CategoryId == categoryId);
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
