using Domain.Entities;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(int id, int quantity)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null) return false;

            inventory.Quantity += quantity;

            return await UpdateInventoryAsync(inventory);
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null) return false;
            if (inventory.Quantity != 0) return false; 

            _context.Inventories.Remove(inventory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Inventory>> GetInventoryAsync()
        {
            return await _context.Inventories
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            return await _context.Inventories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool IsInventoryTableEmpty()
        {
            return !_context.Inventories.Any();
        }

        public async Task<bool> Remove(int id, int quantity)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null) return false;

            inventory.Quantity -= quantity;

            return await UpdateInventoryAsync(inventory);
        }

        private async Task<bool> UpdateInventoryAsync(Inventory inventory)
        {
            _context.Entry(inventory).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
