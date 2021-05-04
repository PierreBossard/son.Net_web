using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SO=System.IO.File;
using son.Net_web.Services;


namespace son.Net_web.Controllers
{
    [ApiController]
    [Route("sound")]
    
    public class Sound : ControllerBase
    {
        private readonly IHubContext<RingHub> _hubContext;

        public Sound(IHubContext<RingHub> hubContext)
        {
            _hubContext = hubContext;
        }

        private RingService service = new RingService();
        
        [HttpGet]
        public async Task Get()
        {
            Models.Ring ring = new Models.Ring();

            ring.date = Timestamp.GetCurrentTimestamp(); 
            
            await service.AddRing(ring);
            await service.RetrieveRings();

            await _hubContext.Clients.All.SendAsync("ReceiveNotifications", "Test");
        }
    }

}