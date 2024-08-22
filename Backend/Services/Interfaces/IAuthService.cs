using Backend.Model.DTOs;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IAuthService
    {
        Task<bool> ChangePassword(ChangePasswordDto dto);
    }
}
