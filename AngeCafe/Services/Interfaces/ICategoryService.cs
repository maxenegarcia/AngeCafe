using AngeCafe.API.Models;

namespace AngeCafe.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task<bool> DeleteAsync(int id);
    }
}
