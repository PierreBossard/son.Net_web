using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace son.Net_web.Controllers
{
    public class RingHub : Hub
    {
        public async Task SendNotifications()
        {
            await Clients.All.SendAsync("ReceiveNotifications", "Ding Dong");
        }
    }
}