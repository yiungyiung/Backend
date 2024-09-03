using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AdminService(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<IEnumerable<object>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Select(u => new
                {
                    u.UserId,
                    u.Email,
                    u.RoleId,
                    u.Contact_Number,
                    u.Name,
                    u.IsActive,
                    Role = new
                    {
                        u.Role.RoleId,
                        u.Role.RoleName
                    }
                })
                .ToListAsync();
        }

        public async Task<User> AddUserAsync(User user)
        {
            var role = await _context.Roles.FindAsync(user.RoleId);
            if (role == null)
            {
                throw new ArgumentException($"Role with id {user.RoleId} not found.");
            }

            user.Role = role;
            String password = GenerateRandomPassword();
            user.PasswordHash = HashPassword(password);
            user.IsActive = true;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            string subject = "Welcome to Our Application";
            string body = $"Your account has been created. Your temporary password is: {password}. Please change it upon your first login.";
            await _emailService.SendEmailAsync(user.Email, subject, body);
            return user;
        }

        // ... (continuing from the previous AdminService code)

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 16)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }


        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with id {user.UserId} not found.");
            }

            // Only update the fields that are allowed to be changed
            existingUser.Email = user.Email;
            existingUser.Name = user.Name;
            existingUser.Contact_Number = user.Contact_Number;
            existingUser.IsActive = user.IsActive;
            existingUser.RoleId = user.RoleId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error updating user: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                throw;
            }

            return existingUser;
        }
    }
}