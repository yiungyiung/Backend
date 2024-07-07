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
            var role = await _context.Roles.FindAsync(user.RoleId);
            if (role == null)
            {
                throw new ArgumentException($"Role with id {user.RoleId} not found.");
            }

            user.Role = role;
            user.PasswordHash = GenerateRandomPassword();
            user.IsActive = true;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await SendWelcomeEmailAsync(user.Email, user.PasswordHash);
            return user;
        }

        private string GenerateRandomPassword()
        {
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

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with id {user.UserId} not found.");
            }

            // Only update the fields that are allowed to be changed
            existingUser.Name = user.Name;
            existingUser.Contact_Number = user.Contact_Number;
            existingUser.IsActive = user.IsActive;
            existingUser.RoleId = user.RoleId;

            // Don't update Email or PasswordHash here

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error updating user: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                throw; // Rethrow the exception to be handled by the controller
            }

            return existingUser;
        }
    }
    }
