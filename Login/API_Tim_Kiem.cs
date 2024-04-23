using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Firebase.Database;
using Firebase.Auth;

namespace Login
{
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
    public static class API_Tim_Kiem
    {
        public static async Task<List<string>> Search (string tuKhoa)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            List<string> result = new List<string>();
            Query query = db.Collection("Truyen").WhereEqualTo("Tac_gia", tuKhoa.ToUpper());
            Query query1 = db.Collection("Truyen").WhereEqualTo("Ten", tuKhoa.ToUpper());

            // Thực thi truy vấn và lấy kết quả
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // In kết quả
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                result.Add(document.Id);
            }

            snapshot = await query1.GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                result.Add(document.Id);
            }

            return result;
        }

        public static async Task<List<string>> advanceSearch(string tuKhoa, string danhGia, string tacGia, string theLoai, string tinhTrang)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            List<string> result = new List<string>();
            Query query = db.Collection("Truyen").WhereEqualTo("Ten", tuKhoa.ToUpper());

            //Xử lý input
            int rating = 0;
            int status = -1;

            if (danhGia != "Bất kỳ")
            {
                rating = Convert.ToInt32(danhGia[danhGia.Length - 1]);
            }

            if (tinhTrang != "Bất kỳ")
            {
                status = Convert.ToInt32(tinhTrang);
            }
            
            // Thực thi truy vấn và lấy kết quả
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // In kết quả
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Novel novel = document.ConvertTo<Novel>();
                if (novel.avgRating >= rating)
                {
                    if (novel.author == tacGia)
                    {
                        if (novel.type.Contains(theLoai))
                        {
                            if (status != -1 && novel.status == status)
                            {
                                result.Add(document.Id);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
