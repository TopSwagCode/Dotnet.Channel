using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Dotnet.Channel.Services
{
    public class NotificationService
    {
        private readonly Channel<Notification> _notificationChannel;

        public NotificationService()
        {
            _notificationChannel = System.Threading.Channels.Channel.CreateUnbounded<Notification>();
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            await _notificationChannel.Writer.WriteAsync(notification);
        }

        public IAsyncEnumerable<Notification> ReadAllNotificationsAsync(System.Threading.CancellationToken stoppingToken)
        {
            return _notificationChannel.Reader.ReadAllAsync(stoppingToken);
        }
    }
}
