using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using FireSharp.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp;
using Newtonsoft.Json;
using System.Net;
using Firebase.Auth;
using System.Security.Cryptography;
using Google.Cloud.Firestore;
using Firebase.Auth.Providers;
using System.Windows;

namespace thongbao
{
    internal class API_thongbao
    {
    }
    public class Thong_Bao
    {
        public string Noi_dung { get; set; }
        public string TG_gui { get; set; }
        public bool Trang_thai { get; set; }
    }
    public class Album
    {
        public string Tentruyen { get; set; }
        public int Chuong_dangdoc { get; set; }
        public string TG_them { get; set; }
        public int tong_chuong { get; set; }
        public string Tacgia { get; set; }
        public string image { get; set; }
        public string The_loai { get; set; }
        public bool Thong_bao { get; set; }
        public int Trang_thai { get; set; }
    }
    public class Novel
    {
        [FirestoreProperty("Anh")]
        public string coverImg { get; set; }
        [FirestoreProperty("Binh_luan")]
        public int comment { get; set; }
        [FirestoreProperty("Danh_gia")]
        public int numRating { get; set; }
        [FirestoreProperty("Danh_gia_Tb")]
        public int avgRating { get; set; }
        [FirestoreProperty("De_cu")]
        public int recommend { get; set; }
        [FirestoreProperty("Luot_thich")]
        public int like { get; set; }
        [FirestoreProperty("Luot_xem")]
        public int numRead { get; set; }
        [FirestoreProperty("So_chuong")]
        public int cntChapter { get; set; }
        [FirestoreProperty("TG_Dang")]
        public Timestamp times { get; set; }
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
        [FirestoreProperty("ID_nguoidang")]
        public string id_nguoidang { get; set; }
    }
    public class Them_Lay_thongbao
    {

        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public async Task<string> Them_thongbao_danhgia(string idtruyen)
        {
            //Realtime database
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            //Authentication
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                AuthDomain = "healtruyen.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            var client1 = new FirebaseAuthClient(config);
            // Firestore
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            //truy xuất đến idtruyen 
            DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
            //lấy dữ liệu truyện ra
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            var ID_ngdang = "";
            var ten_DN = client1.User.Info.DisplayName;
            string content = $"{ten_DN} đã đánh giá truyện của bạn";
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                ID_ngdang = novel.id_nguoidang;
            }
            System.DateTime currentTime = System.DateTime.UtcNow;

            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + ID_ngdang + "Thong_bao/");
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Body);
            //show data
            var dem = "000";
            foreach (var i in dictionary)
            {
                dem = i.Key;
            }
            var dem1 = Convert.ToInt32(dem);
            dem1 += 1;
            int cnt = 0;
            if (dem1 >= 0 && dem1 < 10)
                cnt = 2;
            else if (dem1 >= 10 && dem1 <= 99)
                cnt = 1;
            else
                cnt = 0;
            dem = "";
            for (int i = 0; i < cnt; i++)
            {
                dem += "0";
            }
            dem += dem1;

            Thong_Bao thong_bao = new Thong_Bao()
            {
                Noi_dung = content,
                TG_gui = currentTime.ToString(),
                Trang_thai = false
            };

            await client.SetAsync("Nguoi_dung/" + ID_ngdang + "Thong_bao/" + dem, thong_bao);
            return dem;
        }
        public async Task<string> Them_thongbao_trlbinhluan(string id_ng_binhluan)
        {
            //Realtime database
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            //Authentication
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                AuthDomain = "healtruyen.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            var client1 = new FirebaseAuthClient(config);
            var ten_DN = client1.User.Info.DisplayName;
            string content = $"{ten_DN} đã trả lời bình luận của bạn";
            System.DateTime currentTime = System.DateTime.UtcNow;
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + id_ng_binhluan + "Thong_bao/");
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Body);
            //show data
            var dem = "000";
            foreach (var i in dictionary)
            {
                dem = i.Key;
            }
            var dem1 = Convert.ToInt32(dem);
            dem1 += 1;
            int cnt = 0;
            if (dem1 >= 0 && dem1 < 10)
                cnt = 2;
            else if (dem1 >= 10 && dem1 <= 99)
                cnt = 1;
            else
                cnt = 0;
            dem = "";
            for (int i = 0; i < cnt; i++)
            {
                dem += "0";
            }
            dem += dem1;

            Thong_Bao thong_bao = new Thong_Bao()
            {
                Noi_dung = content,
                TG_gui = currentTime.ToString(),
                Trang_thai = false
            };

            await client.SetAsync("Nguoi_dung/" + id_ng_binhluan + "Thong_bao/" + dem, thong_bao);
            return dem;

        }
        public async Task<string> Them_thongbao_thich_binhluan(string id_ng_binhluan)
        {
            //Realtime database
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            //Authentication
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                AuthDomain = "healtruyen.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            var client1 = new FirebaseAuthClient(config);
            var ten_DN = client1.User.Info.DisplayName;
            string content = $"{ten_DN} đã thich bình luận của bạn";
            System.DateTime currentTime = System.DateTime.UtcNow;
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + id_ng_binhluan + "Thong_bao/");
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Body);
            //show data
            var dem = "000";
            foreach (var i in dictionary)
            {
                dem = i.Key;
            }
            var dem1 = Convert.ToInt32(dem);
            dem1 += 1;
            int cnt = 0;
            if (dem1 >= 0 && dem1 < 10)
                cnt = 2;
            else if (dem1 >= 10 && dem1 <= 99)
                cnt = 1;
            else
                cnt = 0;
            dem = "";
            for (int i = 0; i < cnt; i++)
            {
                dem += "0";
            }
            dem += dem1;

            Thong_Bao thong_bao = new Thong_Bao()
            {
                Noi_dung = content,
                TG_gui = currentTime.ToString(),
                Trang_thai = false
            };

            await client.SetAsync("Nguoi_dung/" + id_ng_binhluan + "Thong_bao/" + dem, thong_bao);
            return dem;

        }
        public async Task Them_thongbao_album(string idtruyen)
        {
            //Realtime database
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + "Thong_bao_album" + idtruyen);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Body);
            List<string> id_nguoidung = new List<string>();
            foreach (var item in dictionary)
            {
                id_nguoidung.Add(item.Key);
            }
            string ten_truyen = "";
            // Firestore
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            //truy xuất đến idtruyen 
            DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
            //lấy dữ liệu truyện ra
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                ten_truyen = novel.nameNovel;
            }
            string content = $"Truyện {ten_truyen} đã đăng chương mới";
            System.DateTime currentTime = System.DateTime.UtcNow;
            
            foreach(var item in id_nguoidung)
            {
                Thong_Bao thong_bao = new Thong_Bao()
                {
                    Noi_dung = content,
                    TG_gui = currentTime.ToString(),
                    Trang_thai = false
                };
                FirebaseResponse res = await client.GetAsync("Nguoi_dung/" + item + "Thong_bao/");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res.Body);
                //show data
                var dem = "000";
                foreach (var i in dict)
                {
                    dem = i.Key;
                }
                var dem1 = Convert.ToInt32(dem);
                dem1 += 1;
                int cnt = 0;
                if (dem1 >= 0 && dem1 < 10)
                    cnt = 2;
                else if (dem1 >= 10 && dem1 <= 99)
                    cnt = 1;
                else
                    cnt = 0;
                dem = "";
                for (int i = 0; i < cnt; i++)
                {
                    dem += "0";
                }
                dem += dem1;
                await client.SetAsync("Nguoi_dung/" + item + "Thong_bao/" + dem, thong_bao);
            }    
        }
        public async Task Tat_Bat_thongbao_album(string userId, string idtruyen, string idchuong, 
            string tentruyen, string tacgia, string anh, string theloai, int status, int sochuong, bool thongbao)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            // Xác định đường dẫn để xóa trong Realtime Database
            var path = $"Nguoi_dung/{userId}/Album/{idtruyen}";  
            // Get the current time
            System.DateTime currentTime = System.DateTime.UtcNow;
            // Convert the current time to a Unix timestamp
           // long timestamp = (long)(currentTime - new System.DateTime(1970, 1, 1)).TotalSeconds;
            Album alb = new Album()
            {
                Tentruyen = tentruyen,
                Chuong_dangdoc = Convert.ToInt32(idchuong),
                TG_them = currentTime.ToString(),
                tong_chuong = sochuong,
                Tacgia = tacgia,
                image = anh,
                The_loai = theloai,
                Thong_bao = thongbao,
                Trang_thai = status
            };

            await client.UpdateAsync(path, alb);
        }
        public async Task<Dictionary<string, Dictionary<string, object>>> Lay_thongbao(string userId)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "Nguoi_dung/" + userId + "/Thong_bao/";
            FirebaseResponse res = await client.GetAsync(path);
            // Chuyển đổi dữ liệu trả về thành Dictionary<string, Dictionary<string, object>>
            Dictionary<string, Dictionary<string, object>> thongbao = new Dictionary<string, Dictionary<string, object>>();
            if (res.Body != "null")
            {
                
                thongbao = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(res.Body);
            }
            return thongbao;
        }
    }
}
