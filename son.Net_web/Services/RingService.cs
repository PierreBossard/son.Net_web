
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
    //class qui implémente les méthodes lié à l'objet Ring
    public class RingService
    {
        //connexion à la base de donnée Firestore
        static string jsonString = SO.ReadAllText("son-net-2f41b-65e974d96873.json");
        static FirestoreClientBuilder builder = new FirestoreClientBuilder {JsonCredentials = jsonString};

        //connexion à la table (collection) qui nous intéresse
        static FirestoreDb db = FirestoreDb.Create("son-net-2f41b", builder.Build());
        CollectionReference docCollec = db.Collection("ring");
        
        //méthode qui permet d'ajouter un nouvel appel dans la base de donnée
        public async Task AddRing(Ring ring)
                    {
                        await docCollec.AddAsync(new Dictionary<string, object>()
                        {
                            {"date", ring.date},
                        });
                    }
        //méthode qui permet de récupérer l'historique des appels (25 max) dans la base de donnée
        public async Task<List<Ring>> RetrieveRings()
        {
            //créer une liste d'ojet Ring
            List<Ring> list = new List<Ring>();
            
            //récupère l'historique des appels, ordonnée avec la date et limité à 25
            Query allRingsQuery = db.Collection("ring").OrderByDescending("date").Limit(25);
            
            //récupère toutes les documents de la requête précèdente
            QuerySnapshot allRingsQuerySnapshot = await allRingsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allRingsQuerySnapshot.Documents)
            {
                Dictionary<string, object> ring = documentSnapshot.ToDictionary();
                //pour chaque document(chaque appel de l'historique)
                foreach (KeyValuePair<string, object> pair in ring)
                {
                    //créer un objet, l'instancie avec la date du document en cours et l'ajoute à la liste
                    Ring r = new Ring();
                    r.date = pair.Value.ToString();
                    list.Add(r);
                }
            }
            return list;
        }
    }
}