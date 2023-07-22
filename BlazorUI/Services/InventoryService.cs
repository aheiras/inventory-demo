using BlazorUI.Pages;
using Domain.Entities;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using System.Text;
using Inventory = Domain.Entities.Inventory;

namespace BlazorUI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _http;

        public InventoryService(HttpClient http)
        {
            _http = http;
        }


        public async Task<bool> Add(int id, int quantity)
        {
            var result = await _http.PutAsync($"api/Inventory/Add/{id}/{quantity}", null);
            return result.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var result = await _http.PostAsync("api/Product/Create", content);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Product>(responseContent);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = await _http.DeleteAsync($"api/Product/Delete/{id}");
            return result.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<IEnumerable<Inventory?>> GetInventoryAsync()
        {
            var result = await _http.GetAsync($"api/Inventory/All");
            var responseContent = await result.Content.ReadAsStringAsync();
            var deserilized = JsonSerializer.Deserialize<IEnumerable<Inventory?>>(responseContent);
            return deserilized ?? new List<Inventory?>();
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int id)
        {
            var result = await _http.GetAsync($"api/Inventory/ById/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Inventory>(responseContent);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Remove(int id, int quantity)
        {
            var result = await _http.PutAsync($"api/Inventory/Remove/{id}/{quantity}", null);
            return result.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var result = await _http.PutAsync($"api/Product/Update/{id}", content);
            return result.StatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
}
