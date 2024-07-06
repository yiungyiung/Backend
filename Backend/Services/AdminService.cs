using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using System.Net.Mail;
using System.Net;

namespace Backend.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AdminService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            user.RoleId = user.RoleId + 1;
            var role = await _context.Roles.FindAsync(user.RoleId);
            if (role == null)
            {
                throw new ArgumentException($"Role with id {user.RoleId} not found.");
            }

            // Assign the role to the user
            user.Role = role;
            user.PasswordHash = GenerateRandomPassword();
            user.IsActive = true;
            // Add user to context and save changes
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await SendWelcomeEmailAsync(user.Email, user.PasswordHash);
            return user;
        }

        private string GenerateRandomPassword()
        {
            // Generate a random string for password (example only, replace with your own logic)
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return password;
        }
        public async Task SendWelcomeEmailAsync(string email, string password)
        {
            var smtpServer = _configuration["SmtpSettings:Server"];
            var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
            var smtpUsername = _configuration["SmtpSettings:Username"];
            var smtpPassword = _configuration["SmtpSettings:Password"];

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    Subject = "Welcome to Our Application",
                    Body = $"Your account has been created. Your temporary password is: {password}. Please change it upon your first login.",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
