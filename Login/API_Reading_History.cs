using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using FireSharp.Interfaces;
using Google.Cloud.Firestore;
using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp;
using Newtonsoft.Json;
using System.Net;
using AlbumTruyen;

namespace Readinghistory
{
    internal static class API_readinghistory
    {

    }
    public class Lich_su_doc
    {
        public string ten_truyen { get; set; }
        public int Chuong_doc_cuoi { get; set; }
        public string TG_doccuoi { get; set; }
        public int tong_chuong { get; set; }
        public string Tacgia { get; set; }
        public string image { get; set; }


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
        [FirestoreProperty("Diem_danhgia")]
        public int tong_DG { get; set; }
        [FirestoreProperty("ID_nguoi_dang")]
        public string id_nguoidang { get; set; }
    }
    public class CRUD_lsd
    {

        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public async Task<Dictionary<string, Dictionary<string, object>>> Lay_lichsudoc(string userId)
        {
            /* IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
             FirestoreDb db = FirestoreDb.Create("healtruyen");
             //truy xuất đến idtruyen 
             DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
             //lấy dữ liệu truyện ra
             DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
             var sochuong = 0;
             var tentruyen = "";
             var tacgia = "";
             var anh = "";
             if (snapshot.Exists)
             {
                 Novel novel = snapshot.ConvertTo<Novel>();
                 sochuong = novel.cntChapter;
                 tentruyen = novel.nameNovel;
                 tacgia = novel.author;
                 anh = novel.coverImg;
             }
             //select data from path Nguoi_dung/[Id]
             FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + userId + "lichsudoc/");
             //get data
             Lich_su_doc data = response.ResultAs<Lich_su_doc>();

             DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(data.TG_doccuoi);
             System.DateTime dateTime = dateTimeOffset.DateTime;

             TimeSpan timeDif = System.DateTime.UtcNow - dateTime;
             //xử lý chuôi TimeSpan 2:03:02:32 là 2 ngày, 3 giờ, 2 phút, 32 giây.*/
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "Nguoi_dung/" + userId + "/lichsudoc/";
            FirebaseResponse res = await client.GetAsync(path);
            // Chuyển đổi dữ liệu trả về thành Dictionary<string, Dictionary<string, object>>
            Dictionary<string, Dictionary<string, object>> readingHistory = new Dictionary<string, Dictionary<string, object>>();
            if (res.Body != "null")
            {
                readingHistory = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(res.Body);
            }
            /* // chuyển từ base64 thành ảnh
             byte[] imageBytes = Convert.FromBase64String(anh);
             // Tạo đối tượng hình ảnh từ mảng byte
             ImageConverter imageConverter = new ImageConverter();
             Image image = (Image)imageConverter.ConvertFrom(imageBytes);*/
            return readingHistory;
        }

        public async Task Capnhat_lichsudoc(string userId, int idchuong, string idtruyen)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            //truy xuất đến idtruyen 
            DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
            //lấy dữ liệu truyện ra
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            int sochuong = 0;
            var tentruyen = "";
            var tacgia = "";
            var anh = "";
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                sochuong = novel.cntChapter;
                tentruyen = novel.nameNovel;
                tacgia = novel.author;
                anh = novel.coverImg;
            }
            // Get the current time
            System.DateTime currentTime = System.DateTime.Now;
            // Convert the current time to a Unix timestamp
            string timestamp = DateTime.Now.ToString();

            // Tạo một đối tượng chứa các giá trị cần cập nhật
            Lich_su_doc lsd = new Lich_su_doc()
            {
                ten_truyen = tentruyen,
                Chuong_doc_cuoi = idchuong,
                TG_doccuoi = timestamp,
                tong_chuong = sochuong,
                Tacgia = tacgia,
                image = anh
            };

            // Xác định đường dẫn cập nhật trong Realtime Database
            var path = $"Nguoi_dung/{userId}/lichsudoc/{idtruyen}";

            // Kiểm tra sự tồn tại của nút tại đường dẫn
            var snshot = await client.GetAsync(path);

            if (snshot != null)
            {
                // Nút tồn tại, thực hiện cập nhật
                await client.UpdateAsync(path, lsd);
            }
            else
            {
                // Nút không tồn tại, thực hiện tạo mới
                await client.SetAsync(path, lsd);
            }
        }
        /*public async Task Them_lichsudoc(string userId, string idtruyen, string idchuong)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            //truy xuất đến idtruyen 
            DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
            //lấy dữ liệu truyện ra
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            var sochuong = 0;
            var tentruyen = "";
            var tacgia = "";
            var anh = "";
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                sochuong = novel.cntChapter;
                tentruyen = novel.nameNovel;
                tacgia = novel.author;
                anh = novel.coverImg;
            }
            // Get the current time
            System.DateTime currentTime = System.DateTime.UtcNow;
            // Convert the current time to a Unix timestamp
            long timestamp = (long)(currentTime - new System.DateTime(1970, 1, 1)).TotalSeconds;

            Lich_su_doc lsd = new Lich_su_doc()
            {
                ten_truyen = tentruyen,
                Chuong_doccuoi  = Convert.ToInt32(idchuong),
                TG_doccuoi = timestamp,
                tong_chuong = sochuong,
                Tacgia = tacgia,
                image = anh
            };

           // FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + userId + "lichsudoc/");
            //var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Body);
            //get data
            // Data data = response.ResultAs<Data>();
            //show data/
         *//*   var dem = "";
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
            dem += dem1;*//*

            SetResponse res = await client.SetAsync("Nguoi_dung/" + userId + "lichsudoc/" + idtruyen, lsd);
            
        }*/

        public async Task xoa_lichsudoc(string userId, string idtruyen)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            // Xác định đường dẫn để xóa trong Realtime Database
            var path = $"Nguoi_dung/{userId}/lichsudoc/{idtruyen}";
            await client.DeleteAsync(path);
        }
        private async void Xoa_truyen_theo_idtruyen(string idtruyen, string uid)
        {
            CRUD_album album = new CRUD_album();
            await album.xoa_album(uid, idtruyen);
        }
    }
}
