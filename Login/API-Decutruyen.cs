using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System.Net;
using System.Reactive;
using System.CodeDom.Compiler;
using Firebase.Auth;

namespace BXH
{
    internal class API_Decutruyen
    {
    }
    public class Novel
    {
        [FirestoreProperty("Anh")]
        public string coverImg { get; set; }
        [FirestoreProperty("De_cu")]
        public int recommend { get; set; }
        [FirestoreProperty("Luot_xem")]
        public int numRead { get; set; }
        [FirestoreProperty("Tac_gia")]
        public string author { get; set; }
        [FirestoreProperty("Ten")]
        public string nameNovel { get; set; }
        [FirestoreProperty("The_loai")]
        public string[] type { get; set; }
        [FirestoreProperty("Tom_tat")]
        public string description { get; set; }
        [FirestoreProperty("Trang_thai")]
        public int status { get; set; }
    }

    public class BXH
    {
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public async Task<List<Novel>> SortBXH_decu(string TOP, string ID_truyen)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "BXH/De_cu/Luot_decu" + TOP + ID_truyen;
            FirebaseResponse res = await client.GetAsync(path);
            List<Novel> sortedBXH = new List<Novel>();
            if (res.Body != "null")
            {
                var novels = JsonConvert.DeserializeObject<List<Novel>>(res.Body);
                sortedBXH = novels.OrderByDescending(n => n.recommend).ToList();
            }
            return sortedBXH;
        }

        public async Task<List<Novel>> SortBXH_views(string TOP, string ID_truyen)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "BXH/De_cu/Luot_view" + TOP + ID_truyen;
            FirebaseResponse res = await client.GetAsync(path);
            List<Novel> sortedBXH = new List<Novel>();
            if (res.Body != "null")
            {
                var novels = JsonConvert.DeserializeObject<List<Novel>>(res.Body);
                sortedBXH = novels.OrderByDescending(n => n.numRead).ToList();
            }
            return sortedBXH;
        }   

        public async Task<List<Novel>> SortBXH_dangxem(string TOP, string ID_truyen)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "BXH/De_cu/All" + TOP + ID_truyen;
            FirebaseResponse res = await client.GetAsync(path);
            List<Novel> novels = new List<Novel>();

            if (res.Body != "null")
            {
                novels = JsonConvert.DeserializeObject<List<Novel>>(res.Body);
            }

            // Custom sorting
            novels.Sort((a, b) =>
            {
                // Calculate 'recommend' score based on the given formula
                int ascore = a.recommend + a.numRead * 1000;
                int bscore = b.recommend + b.numRead * 1000;
                // Compare the scores
                return bscore.CompareTo(ascore);
            });

            return novels;
        }
    }
}