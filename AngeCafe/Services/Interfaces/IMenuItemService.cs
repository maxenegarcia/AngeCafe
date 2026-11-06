using AngeCafe.API.Models;

namespace AngeCafe.API.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task<List<MenuItem>> GetAllAsync();

        Task<MenuItem?> GetByIdAsync(int id);

        Task AddAsync(MenuItem menuItem);

        Task UpdateAsync(MenuItem menuItem);

        Task<bool> DeleteAsync(int id);
    }
}
