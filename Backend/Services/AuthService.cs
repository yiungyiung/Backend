using Backend.Model;
using Backend.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);

            if (user == null)
                return false;

            using (var sha256 = SHA256.Create())
            {
                var oldPasswordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.OldPassword)));

                if (oldPasswordHash != user.PasswordHash)
                    return false;

                var newPasswordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.NewPassword)));
                user.PasswordHash = newPasswordHash;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
