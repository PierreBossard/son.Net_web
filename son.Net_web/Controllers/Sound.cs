using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SO=System.IO.File;

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
        
        
        [HttpGet]
        public async Task<string> Get()
        {
            var jsonString = SO.ReadAllText("son-net-2f41b-65e974d96873.json");
            var builder = new FirestoreClientBuilder {JsonCredentials = jsonString};
            FirestoreDb db = FirestoreDb.Create("son-net-2f41b", builder.Build());

            CollectionReference docCollec = db.Collection("ring");
            
            Dictionary<string, object> ring = new Dictionary<string, object>
            {    
                //Add datta 
                { "date", Timestamp.GetCurrentTimestamp() },
                { "state", true}
            };

            await _hubContext.Clients.All.SendAsync("ReceiveNotifications", "Test");
            
            var res = await docCollec.AddAsync(ring);
            return res.ToString();
        }
    }
}