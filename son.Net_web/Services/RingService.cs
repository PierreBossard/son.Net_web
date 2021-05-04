
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using SO=System.IO.File;

namespace son.Net_web.Services
{
    public class RingService
    {
        static string jsonString = SO.ReadAllText("son-net-2f41b-65e974d96873.json");
        static FirestoreClientBuilder builder = new FirestoreClientBuilder {JsonCredentials = jsonString};

        static FirestoreDb db = FirestoreDb.Create("son-net-2f41b", builder.Build());
        CollectionReference docCollec = db.Collection("ring");
        public async Task AddRing(Models.Ring ring)
                    {
                        await docCollec.AddAsync(new Dictionary<string, object>()
                        {
                            {"date", Timestamp.GetCurrentTimestamp()},
                        });
                    }

        public async Task RetrieveRings()
        {
            Query allRingsQuery = db.Collection("ring");
            QuerySnapshot allRingsQuerySnapshot = await allRingsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allRingsQuerySnapshot.Documents)
            {
                Console.WriteLine(documentSnapshot.Id);
                Dictionary<string, object> ring = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ring)
                {
                    Console.WriteLine("{1} \n", pair.Key, pair.Value);
                }
            }
        }
    }
}