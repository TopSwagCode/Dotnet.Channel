using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dotnet.Channel.Services;

namespace Dotnet.Channel.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;
        private readonly NotificationService _notificationService;

        public ErrorModel(ILogger<ErrorModel> logger, NotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
            
        }

        public async Task OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            await _notificationService.SendNotificationAsync(new Notification
            {
                Message = $"User got error with RequestId: {RequestId}",
                Group = "Admins"
            });
        }
    }
}
