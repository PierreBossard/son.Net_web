using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using SO=System.IO.File;
using Google.Cloud.Firestore.V1;

namespace son.Net_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Firebase : ControllerBase
    {
        
        [HttpGet]
        public async Task<string> Get()
        {
            var jsonString = SO.ReadAllText("son-net-2f41b-65e974d96873.json");
            var builder = new FirestoreClientBuilder {JsonCredentials = jsonString};
            FirestoreDb db = FirestoreDb.Create("son-net-2f41b", builder.Build());

            DocumentReference docRef = db.Collection("ring").Document("1ZODNCZC");
            
            Dictionary<string, object> ring = new Dictionary<string, object>
            {
                { "date", Timestamp.GetCurrentTimestamp() },
                { "state", true}
                //Add datta 
            };
            
            var res = await docRef.UpdateAsync(ring);
            
            return res.ToString();
        }
    }
}