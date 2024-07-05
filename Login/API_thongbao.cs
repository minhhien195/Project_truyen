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
    }
    public class Album
    {
        public string Tentruyen { get; set; }
        public int Chuong_dang_doc { get; set; }
        public string TG_them { get; set; }
        public string Tacgia { get; set; }
        public string The_loai { get; set; }
        public bool Thong_bao { get; set; }
        public int Trang_thai { get; set; }
    }
    [FirestoreData]
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
        [FirestoreProperty("Diem_danhgia")]
        public int tong_DG { get; set; }
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
        [FirestoreProperty("ID_nguoi_dang")]
        public string id_nguoidang { get; set; }
    }
    public class Them_Lay_thongbao
    {

        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public async Task<string> Them_thongbao_danhgia(string idtruyen, int dg_decu)
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
            var tentruyen = "";
            var ten_DN = client1.User.Info.DisplayName;
            string content = "";
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                ID_ngdang = novel.id_nguoidang;
                tentruyen = novel.nameNovel;
            }
            System.DateTime currentTime = System.DateTime.Now;
            if (dg_decu == 0)
                content = $"{ten_DN} đã đánh giá truyện {tentruyen} của bạn";
            else if (dg_decu == 1)
            {
                content = $"{ten_DN} đã đề cử truyện {tentruyen} của bạn";
            }
            
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + ID_ngdang + "/Thong_bao");
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
                TG_gui = currentTime.ToString()
            };

            await client.SetAsync("Nguoi_dung/" + ID_ngdang + "/Thong_bao/" + dem, thong_bao);
            return dem;
        }
        
        public async Task<string> Thongbao_Duyettruyen(string idtruyen, bool tc_xn)
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
            DocumentReference docReference = db.Collection("Dang_truyen").Document(idtruyen);
            //lấy dữ liệu truyện ra
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            var ID_ngdang = "";
            var tentruyen = "";
            string content = "";
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                ID_ngdang = novel.id_nguoidang;
                tentruyen = novel.nameNovel;
            }
            System.DateTime currentTime = System.DateTime.Now;
            if (tc_xn == false)
                content = $"Admin đã từ chối thông qua truyện {tentruyen} của bạn";
            else
            {
                content = $"Admin đã thông qua truyện {tentruyen} của bạn";
            }
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + ID_ngdang + "/Thong_bao");
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
                TG_gui = currentTime.ToString()
            };

            await client.SetAsync("Nguoi_dung/" + ID_ngdang + "/Thong_bao/" + dem, thong_bao);
            return dem;
        }
        public async Task<string> Them_thongbao_canhcao(string id_ngvipham, int slVipham)
        {
            //Realtime database
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);

            System.DateTime currentTime = System.DateTime.Now;
            string content = "";
            content = $"Bạn đã vi phạm điều lệ sử dụng ứng dụng đọc truyện chung. Cảnh cáo lần {slVipham}";

            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + id_ngvipham + "/Thong_bao");
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
                TG_gui = currentTime.ToString()
            };

            await client.SetAsync("Nguoi_dung/" + id_ngvipham + "/Thong_bao/" + dem, thong_bao);
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
            System.DateTime currentTime = System.DateTime.Now;
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
                TG_gui = currentTime.ToString()
            };

            await client.SetAsync("Nguoi_dung/" + id_ng_binhluan + "Thong_bao/" + dem, thong_bao);
            return dem;

        }
        public async Task Them_thongbao_album(string idtruyen, string idchuong, bool capnhat_dangmoi)
        {
            //Realtime database
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/Thong_bao_album/" + idtruyen);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Body);
            List<string> id_nguoidung = new List<string>();
            if (dictionary != null)
            {
                foreach (var item in dictionary.Keys)
                {
                    id_nguoidung.Add(item);
                }
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
            string content = "";
            if (capnhat_dangmoi == true)
                content = $"Truyện {ten_truyen} đã đăng chương mới";
            else
                content = $"Truyện {ten_truyen} đã cập nhật chương {idchuong}";
            System.DateTime currentTime = System.DateTime.Now;
            
            foreach(var item in id_nguoidung)
            {
                Thong_Bao thong_bao = new Thong_Bao()
                {
                    Noi_dung = content,
                    TG_gui = currentTime.ToString()
                };
                FirebaseResponse res = await client.GetAsync("Nguoi_dung/" + item + "/Thong_bao/");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res.Body);

                // Đếm số thông báo hiện có
                int dem1 = 0;
                if (dict != null)
                {
                    foreach (var i in dict)
                    {
                        if (int.TryParse(i.Key, out int currentKey))
                        {
                            dem1 = Math.Max(dem1, currentKey);
                        }
                    }
                }

                dem1 += 1;
                string dem = dem1.ToString("D3"); // Chuyển đổi sang dạng "000", "001", ...
                await client.SetAsync("Nguoi_dung/" + item + "/Thong_bao/" + dem, thong_bao);
            }    
        }
        public async Task Tat_Bat_thongbao_album(string userId, string idtruyen, string idchuong, 
            bool thongbao)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            // Xác định đường dẫn để xóa trong Realtime Database
            var path = $"Nguoi_dung/{userId}/Album/{idtruyen}";  
            // Get the current time
            System.DateTime currentTime = System.DateTime.Now;
            // Convert the current time to a Unix timestamp
            Album alb = new Album()
            {
                Chuong_dang_doc = Convert.ToInt32(idchuong),
                TG_them = currentTime.ToString(),
                Thong_bao = thongbao,
            };

            await client.UpdateAsync(path, alb);
        }
        public async Task<Dictionary<string, Dictionary<string, object>>> Lay_thongbao(string userId)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "Nguoi_dung/" + userId + "/Thong_bao";
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
