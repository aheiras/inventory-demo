using Domain.Entities;

namespace BlazorUI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User?>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> LoginAsync(string email, string pass);
    }
}
