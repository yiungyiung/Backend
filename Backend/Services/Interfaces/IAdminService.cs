using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<object>> GetAllUsersAsync();
        Task<User> AddUserAsync(User user);
        Task SendWelcomeEmailAsync(string email, string password);
        Task<User> UpdateUserAsync(User user);
    }
}
