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

    public class API_Tim_Kiem
    {

        public async Task<List<string>> Search(string tuKhoa)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            List<string> result = new List<string>();

            Query query = db.Collection("Truyen").WhereArrayContainsAny("Tac_gia", tuKhoa.ToUpper());

            Query query1 = db.Collection("Truyen").WhereArrayContainsAny("Ten", tuKhoa.ToUpper());

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

        public async Task<List<string>> AdvanceSearch(string tuKhoa, double danhGia, string tacGia, string theLoai, int tinhTrang)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            List<string> result = new List<string>();
            Query query = db.Collection("Truyen");

            bool isName = false;
            bool isDG = false;
            bool isTG = false;
            bool isTL = false;
            bool isTT = false;

            if (!string.IsNullOrEmpty(tuKhoa) && tuKhoa.ToUpper() != "TÌM KIẾM")
            {
                query = query.WhereEqualTo("Ten", tuKhoa.ToUpper());
                isName = true;
            }

            if (danhGia != -1)
            {
                query = query.WhereLessThanOrEqualTo("Danh_gia_Tb", danhGia);
                isDG = true;
            }

            if (!string.IsNullOrEmpty(tacGia))
            {
                query = query.WhereEqualTo("Tac_gia", tacGia);
                isTG = true;
            }

            if (!string.IsNullOrEmpty(theLoai) && theLoai.ToUpper() != "KHÔNG")
            {
                query = query.WhereArrayContains("The_loai", theLoai);
                isTL = true;
            }

            if (tinhTrang != -1)
            {
                query = query.WhereEqualTo("Trang_thai", tinhTrang);
                isTT = true;
            }

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                bool allc = true;

                if (isName && !document.GetValue<string>("Ten").ToUpper().Contains(tuKhoa.ToUpper()))
                {
                    allc = false;
                    continue;
                }

                if (isDG && document.GetValue<double>("Danh_gia_Tb") > danhGia)
                {
                    allc = false;
                    continue;
                }

                if (isTG && document.GetValue<string>("Tac_gia") != tacGia)
                {
                    allc = false;
                    continue;
                }

                if (isTL)
                {
                    List<string> theLoaiList = new List<string>();

                    // Assuming "The_loai" is an array in Firebase
                    foreach (object theLoaiItem in (List<object>)document.GetValue<object>("The_loai"))
                    {
                        // Chuyển đổi mỗi phần tử thành string và chuyển thành chữ hoa
                        theLoaiList.Add(theLoaiItem.ToString().ToUpper());
                    }

                    // Check if theLoai is in the theLoaiList
                    bool isInTheLoaiList = theLoaiList.Contains(theLoai.ToUpper());

                    if (!isInTheLoaiList)
                    {
                        allc = false;
                        continue;
                    }
                }

                if (isTT && document.GetValue<int>("Trang_thai") != tinhTrang)
                {
                    allc = false;
                    continue;
                }

                if (allc)
                {
                    result.Add(document.Id);
                }
            }
            return result;
        }
    }
}
