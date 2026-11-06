using AngeCafe.API.Models;
using AngeCafe.API.Repositories.Interfaces;
using AngeCafe.API.Services.Interfaces;

namespace AngeCafe.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new ArgumentException("Category name is required.");
            }

            await _repository.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new ArgumentException("Category name is required.");
            }

            var existingCategory =
                await _repository.GetByIdAsync(category.CategoryId);

            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            existingCategory.CategoryName = category.CategoryName;

            await _repository.UpdateAsync(existingCategory);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return false;
            }

            if (await _repository.HasMenuItemsAsync(id))
            {
                throw new InvalidOperationException(
                    "Cannot delete category because it contains menu items.");
            }

            await _repository.DeleteAsync(category);

            return true;
        }
    }
}
