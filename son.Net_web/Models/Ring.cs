using System;
using Google.Cloud.Firestore;

namespace son.Net_web.Models
{
    [FirestoreData]
    //Class qui créer l'objet Ring
    public class Ring
    {
        //définition de ses attributs
        [FirestoreProperty]
        public String date { get; set;}
    }
}