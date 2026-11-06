using AngeCafe.API.Models;
using AngeCafe.API.Repositories.Interfaces;
using AngeCafe.API.Services.Interfaces;

namespace AngeCafe.API.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;

        public MenuItemService(IMenuItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            ValidateMenuItem(menuItem);

            if (!await _repository.CategoryExistsAsync(menuItem.CategoryId))
            {
                throw new ArgumentException(
                    "The selected category does not exist.");
            }

            await _repository.AddAsync(menuItem);
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            ValidateMenuItem(menuItem);

            var existingItem =
                await _repository.GetByIdAsync(menuItem.MenuItemId);

            if (existingItem == null)
            {
                throw new KeyNotFoundException("Menu item not found.");
            }

            if (!await _repository.CategoryExistsAsync(menuItem.CategoryId))
            {
                throw new ArgumentException(
                    "The selected category does not exist.");
            }

            existingItem.CategoryId = menuItem.CategoryId;
            existingItem.ItemName = menuItem.ItemName.Trim();
            existingItem.Description = menuItem.Description;
            existingItem.ImageUrl = menuItem.ImageUrl;
            existingItem.IsActive = menuItem.IsActive;

            // Replace existing variants
            existingItem.Variants.Clear();

            foreach (var variant in menuItem.Variants)
            {
                existingItem.Variants.Add(new MenuItemVariant
                {
                    VariantName = variant.VariantName.Trim(),
                    Price = variant.Price,
                    IsAvailable = variant.IsAvailable
                });
            }

            await _repository.UpdateAsync(existingItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menuItem = await _repository.GetByIdAsync(id);

            if (menuItem == null)
            {
                return false;
            }

            // Soft delete: deactivate the menu item.
            menuItem.IsActive = false;

            await _repository.UpdateAsync(menuItem);

            return true;
        }

        private static void ValidateMenuItem(MenuItem menuItem)
        {
            if (string.IsNullOrWhiteSpace(menuItem.ItemName))
            {
                throw new ArgumentException(
                    "Menu item name is required.");
            }

            if (menuItem.Variants == null ||
                !menuItem.Variants.Any())
            {
                throw new ArgumentException(
                    "A menu item must have at least one variant.");
            }

            foreach (var variant in menuItem.Variants)
            {
                if (string.IsNullOrWhiteSpace(variant.VariantName))
                {
                    throw new ArgumentException(
                        "Variant name is required.");
                }

                if (variant.Price < 0)
                {
                    throw new ArgumentException(
                        "Variant price cannot be negative.");
                }
            }
        }
    }
}
