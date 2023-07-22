using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<bool> Add(int id, int quantity)
        {
            return await _inventoryRepository.Add(id, quantity);
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            return await _inventoryRepository.CreateInventoryAsync(inventory);
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            return await _inventoryRepository.DeleteInventoryAsync(id);
        }

        public async Task<IEnumerable<Inventory>> GetInventoryAsync()
        {
            return await _inventoryRepository.GetInventoryAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            return await _inventoryRepository.GetInventoryByIdAsync(id);
        }

        public async Task<bool> Remove(int id, int quantity)
        {
            return await _inventoryRepository.Remove(id, quantity);
        }
    }
}
