
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Google.Protobuf.WellKnownTypes;
using son.Net_web.Models;
using SO=System.IO.File;
using Timestamp = Google.Cloud.Firestore.Timestamp;

namespace son.Net_web.Services
{
    public class RingService
    {
        static string jsonString = SO.ReadAllText("son-net-2f41b-65e974d96873.json");
        static FirestoreClientBuilder builder = new FirestoreClientBuilder {JsonCredentials = jsonString};

        static FirestoreDb db = FirestoreDb.Create("son-net-2f41b", builder.Build());
        CollectionReference docCollec = db.Collection("ring");
        public async Task AddRing(Ring ring)
                    {
                        await docCollec.AddAsync(new Dictionary<string, object>()
                        {
                            {"date", ring.date},
                        });
                    }

        public async Task<List<Ring>> RetrieveRings()
        {
            List<Ring> list = new List<Ring>();
            Query allRingsQuery = db.Collection("ring").OrderByDescending("date").Limit(25);
            QuerySnapshot allRingsQuerySnapshot = await allRingsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allRingsQuerySnapshot.Documents)
            {

               
                Dictionary<string, object> ring = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ring)
                {
                    Ring r = new Ring();
                    r.date = pair.Value.ToString();
                    list.Add(r);
                }
                
               

            }

            return list;
        }
    }
}