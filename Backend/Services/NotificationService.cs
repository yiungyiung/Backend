using Backend.Model;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateNotification(int userId, string message)
        {
            var notification = new Notification
            {
                UserID = userId,
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
        public async Task MarkNotificationAsRead(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;  // Update the status to read
                await _context.SaveChangesAsync();
            }
        }

        // New function: Get all notifications for a specific user
        public async Task<List<Notification>> GetNotificationsForUser(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                // If the user doesn't exist, return an empty list (or handle it in another way if necessary)
                return new List<Notification>();
            }

            // Retrieve notifications for the user, sorted by newest
            return await _context.Notifications
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
