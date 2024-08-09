using Backend.Model;

namespace Backend.Services.Interfaces
{
    public interface IDataService
    {
        IEnumerable<User> GetUsers();
    }
}