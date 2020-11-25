using System.Threading;
using System.Threading.Tasks;
using Dotnet.Channel.Hubs;
using Dotnet.Channel.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dotnet.Channel.HostedServices
{
    public class NotificationHostedService : BackgroundService
    {
        private readonly NotificationService _notificationService;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ILogger<NotificationHostedService> _logger;

        public NotificationHostedService(NotificationService notificationService, IHubContext<NotificationHub> notificationHubContext, ILogger<NotificationHostedService> logger)
        {
            _notificationService = notificationService;
            _notificationHubContext = notificationHubContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var notification in _notificationService.ReadAllNotificationsAsync(stoppingToken))
            {
                _logger.LogInformation($"Sending Notification to Group: {notification.Group}, Message: {notification.Message}");
                await _notificationHubContext.Clients.Group(notification.Group).SendAsync("AdminNotification", notification.Message, stoppingToken);
            }
        }
    }
}
