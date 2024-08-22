
namespace BusinessLayer.Services.Abstraction
{
    public interface INotificationService
    {
        public Task<string> SendPushNotificationAsync(string title, string body, string token);
    }
}

