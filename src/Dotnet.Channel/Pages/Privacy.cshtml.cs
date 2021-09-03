using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet.Channel.Services;

namespace Dotnet.Channel.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public PrivacyModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task OnGet()
        {
            await _notificationService.SendNotificationAsync(new Notification
            {
                Message = "User visited page: Privacy",
                Group = "Admins"
            });
        }
    }
}
