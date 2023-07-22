using Domain.Entities;

namespace BlazorUI.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory?>> GetInventoryAsync();
        Task<Inventory?> GetInventoryByIdAsync(int id);
        Task<bool> Add(int id, int quantity);
        Task<bool> Remove(int id, int quantity);
        Task<Product?> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
