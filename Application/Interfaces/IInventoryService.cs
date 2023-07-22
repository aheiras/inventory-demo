using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetInventoryAsync();
        Task<Inventory> GetInventoryByIdAsync(int id);
        Task<bool> Add(int id, int quantity);
        Task<bool> Remove(int id, int quantity);
        Task<bool> DeleteInventoryAsync(int id);
    }
}
