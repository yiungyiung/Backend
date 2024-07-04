using Backend.Model;

namespace Backend.Services
{
    public interface IDataService
    {
        IEnumerable<User> GetUsers();
    }
}