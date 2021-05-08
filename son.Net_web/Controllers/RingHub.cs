using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using son.Net_web.Models;

namespace son.Net_web.Controllers
{
    public class RingHub : Hub
    {
        public async Task SendNotifications(String message, List<Ring> list)
        {
            await Clients.All.SendAsync("ReceiveNotifications", message, list);
        }
    }
}