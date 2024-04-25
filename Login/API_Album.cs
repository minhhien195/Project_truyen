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


namespace Album_truyen
{
    internal class API_Album
    {
    }
    public class Album
    {
        public string Tentruyen { get; set; }
        public int Chuong_dangdoc { get; set; }
        public long TG_them { get; set; }
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
    }
    public class CRUD_lsd
    {
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public async Task<List<Dictionary<string, Dictionary<string, object>>>> Lay_album(string userId, string idtruyen, string idchuong, bool thongbao)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "Nguoi_dung/" + userId + "Album/";
            FirebaseResponse res = await client.GetAsync(path);
            // Chuyển đổi dữ liệu trả về thành Dictionary<string, Dictionary<string, object>>
            List<Dictionary<string, Dictionary<string, object>>> readingAlbum = new List<Dictionary<string, Dictionary<string, object>>>();
            if (res.Body != "null")
            {
                readingAlbum = JsonConvert.DeserializeObject<List<Dictionary<string, Dictionary<string, object>>>>(res.Body);
            }
            /* // chuyển từ base64 thành ảnh
             byte[] imageBytes = Convert.FromBase64String(anh);
             // Tạo đối tượng hình ảnh từ mảng byte
             ImageConverter imageConverter = new ImageConverter();
             Image image = (Image)imageConverter.ConvertFrom(imageBytes);*/
            return readingAlbum;
        }

        public async Task Capnhat_album(string userId, string idchuong, string idtruyen, bool thongbao)
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
            var theloai = "";
            var status = 0;
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                sochuong = novel.cntChapter;
                tentruyen = novel.nameNovel;
                tacgia = novel.author;
                anh = novel.coverImg;
                theloai = novel.type[0];
                status = novel.status;
            }
            // Get the current time
            System.DateTime currentTime = System.DateTime.UtcNow;
            // Convert the current time to a Unix timestamp
            long timestamp = (long)(currentTime - new System.DateTime(1970, 1, 1)).TotalSeconds;

            // Tạo một đối tượng chứa các giá trị cần cập nhật
            Album alb = new Album()
            {
                Tentruyen = tentruyen,
                Chuong_dangdoc = Convert.ToInt32(idchuong),
                TG_them = timestamp,
                tong_chuong = sochuong,
                Tacgia = tacgia,
                image = anh,
                The_loai = theloai,
                Thong_bao = thongbao,
                Trang_thai = status
            };

            // Xác định đường dẫn cập nhật trong Realtime Database
            var path = $"Nguoi_dung/{userId}/Album/{idtruyen}";

            // Kiểm tra sự tồn tại của nút tại đường dẫn
            var snshot = await client.GetAsync(path);

            if (snshot != null)
            {
                // Nút tồn tại, thực hiện cập nhật
                await client.UpdateAsync(path, alb);
            }
            else
            {
                // Nút không tồn tại, thực hiện tạo mới
                await client.SetAsync(path, alb);
            }
        }
        /*public async Task Them_album(string userId, string idtruyen, string idchuong, bool thongbao)
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
            var theloai = "";
            var status = 0;
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                sochuong = novel.cntChapter;
                tentruyen = novel.nameNovel;
                tacgia = novel.author;
                anh = novel.coverImg;
                theloai = novel.type[0];
                status = novel.status;
            }
            // Get the current time
            System.DateTime currentTime = System.DateTime.UtcNow;
            // Convert the current time to a Unix timestamp
            long timestamp = (long)(currentTime - new System.DateTime(1970, 1, 1)).TotalSeconds;

            // Tạo một đối tượng chứa các giá trị cần cập nhật
            Album alb = new Album()
            {
                Tentruyen = tentruyen,
                Chuong_dangdoc = Convert.ToInt32(idchuong),
                TG_them = timestamp,
                tong_chuong = sochuong,
                Tacgia = tacgia,
                image = anh,
                The_loai = theloai,
                Thong_bao = thongbao,
                Trang_thai = status
            };
            SetResponse res = await client.SetAsync("Nguoi_dung/" + userId + "Album/" + idtruyen, alb);

        }*/

        public async Task<bool> xoa_album(string userId, string idtruyen)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            // Xác định đường dẫn để xóa trong Realtime Database
            var path = $"Nguoi_dung/{userId}/Album/{idtruyen}";
            var response = await client.DeleteAsync(path);
            return response.StatusCode == HttpStatusCode.OK;
        }

    }
}
