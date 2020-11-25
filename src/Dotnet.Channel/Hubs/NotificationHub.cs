using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Dotnet.Channel.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinAdmin()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            await Clients.Group("Admins").SendAsync("AdminNotification", $"{Context.ConnectionId} has joined the group: Admins.");
        }
    }
}
