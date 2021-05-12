using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using son.Net_web.Models;
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
            List<Ring> list = new List<Ring>();
            
            Ring ring = new Ring();
            ring.date = DateTime.Now.ToString();
            await service.AddRing(ring);
            list = await service.RetrieveRings();

            await _hubContext.Clients.All.SendAsync("ReceiveNotifications", "Ding Dong", list);
        }
    }

}