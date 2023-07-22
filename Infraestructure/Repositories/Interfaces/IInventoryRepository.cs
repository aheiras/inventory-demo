using Domain.Entities;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetInventoryAsync();
        Task<Inventory> GetInventoryByIdAsync(int id);
        Task<Inventory> CreateInventoryAsync(Inventory inventory);
        Task<bool> Add(int id, int quantity);
        Task<bool> Remove(int id, int quantity);
        Task<bool> DeleteInventoryAsync(int id);
        bool IsInventoryTableEmpty();
    }
}
