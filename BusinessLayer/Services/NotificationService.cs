using BusinessLayer.Services.Abstraction;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotificationService : INotificationService
    {
        public async Task<string> SendPushNotificationAsync(string title, string body, string token)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body,
                },
                Token = token,
            };
         
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return response;
        }
    }
}

