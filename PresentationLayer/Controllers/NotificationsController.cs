using BusinessLayer.DTOs;
using BusinessLayer.Services;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService) 
        {
            _notificationService = notificationService;
        }

        [HttpPost("send-notification")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest notification)
        {
            var result = await _notificationService.SendPushNotificationAsync(notification.Title, notification.Body, notification.Token);
            return Ok(result);
        }
    }
}

