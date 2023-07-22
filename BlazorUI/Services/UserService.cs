using Domain.Entities;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace BlazorUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var result = await _http.PostAsync("api/User/Create", content);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(responseContent);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var result = await _http.DeleteAsync($"api/User/Delete/{id}");
            return result.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var result = await _http.GetAsync($"api/User/ById/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(responseContent);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<User?>> GetUsersAsync()
        {
            var result = await _http.GetAsync($"api/User/All");
            var responseContent = await result.Content.ReadAsStringAsync();
            var deserilized = JsonSerializer.Deserialize<IEnumerable<User?>>(responseContent);
            return deserilized ?? new List<User?>();
        }

        public async Task<User?> LoginAsync(string email, string pass)
        {
            _http.DefaultRequestHeaders.Add("email", email);
            _http.DefaultRequestHeaders.Add("password", pass);

            var result = await _http.GetAsync("api/User/Login");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User?>(responseContent);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var result = await _http.PutAsync($"api/User/Update/{id}", content);
            return result.StatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
}
