using System;
using Google.Cloud.Firestore;

namespace son.Net_web.Models
{
    [FirestoreData]
    public class Ring
    {
        [FirestoreProperty]
        public String date { get; set;}
    }
}