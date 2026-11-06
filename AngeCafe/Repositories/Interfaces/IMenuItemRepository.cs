using AngeCafe.API.Models;

namespace AngeCafe.API.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetAllAsync();

        Task<MenuItem?> GetByIdAsync(int id);

        Task<bool> CategoryExistsAsync(int categoryId);

        Task AddAsync(MenuItem menuItem);

        Task UpdateAsync(MenuItem menuItem);

        Task DeleteAsync(MenuItem menuItem);
    }
}
