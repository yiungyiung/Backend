using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // 1. Get all notifications for the authenticated user (vendor)
        [HttpGet("user-notifications/{userID}")]
        public async Task<IActionResult> GetUserNotifications(int userID)
        {
            var notifications = await _notificationService.GetNotificationsForUser(userID);
            return Ok(notifications);
        }

        // 2. Mark a notification as read
        [HttpPut("mark-as-read/{notificationId}")]
        [Authorize(Roles = "Vendor")]  // Authorization for vendors
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            await _notificationService.MarkNotificationAsRead(notificationId);
            return Ok(new { Message = "Notification marked as read" });
        }
    }
}
