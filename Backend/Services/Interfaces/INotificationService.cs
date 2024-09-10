using Backend.Model;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotification(int userId, string message);
        Task MarkNotificationAsRead(int notificationId);  // New function to mark notification as read
        Task<List<Notification>> GetNotificationsForUser(int userId);
    }
}