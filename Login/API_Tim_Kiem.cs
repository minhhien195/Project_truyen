using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Firebase.Database;
using Firebase.Auth;
using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2019.Presentation;

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
        [FirestoreProperty("ID_nguoi_dang")]
        public string id_nguoidang { get; set; }
    }

    public class API_Tim_Kiem
    {

        public async Task<List<string>> Search(string tuKhoa)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            List<string> result = new List<string>();

            QuerySnapshot ds = await db.Collection("Truyen").GetSnapshotAsync();

            Dictionary<string, string> dsach = new Dictionary<string, string>();

            foreach (var item in ds.Documents)
            {
                dsach.Add(item.Id, item.GetValue<string>("Ten"));
            }

            foreach(var item in dsach)
            {
                if (item.Value.Contains(tuKhoa.ToUpper()))
                {
                    result.Add(item.Key);
                }
            }

            return result;
        }

        public async Task<List<string>> AdvanceSearch(string tuKhoa, double danhGia, string tacGia, string theLoai, int tinhTrang)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);   
            List<string> result = new List<string>();

            QuerySnapshot ds = await db.Collection("Truyen").GetSnapshotAsync();

            Dictionary<string, string> dsach = new Dictionary<string, string>();

            Query query = db.Collection("Truyen");

            bool isName = false, isTG = false;
            if (tuKhoa.ToUpper() == "TÌM KIẾM")
            {
                tuKhoa = null;
            }
            if (!string.IsNullOrEmpty(tuKhoa) && string.IsNullOrEmpty(tacGia))
            {
                foreach (var item in ds.Documents)
                {
                    if (item.GetValue<string>("Ten").Contains(tuKhoa.ToUpper()))
                    dsach.Add(item.Id, item.GetValue<string>("Ten"));
                }
                isName = true;
            } else if (string.IsNullOrEmpty(tuKhoa) && !string.IsNullOrEmpty(tacGia)) {
                foreach (var item in ds.Documents)
                {
                    if (item.GetValue<string>("Tac_gia").ToUpper().Contains(tacGia.ToUpper()))
                        dsach.Add(item.Id, item.GetValue<string>("Tac_gia"));
                }
                isTG = true;
            } else if (!string.IsNullOrEmpty(tuKhoa) && !string.IsNullOrEmpty(tacGia)) {
                foreach (var item in ds.Documents)
                {
                    if (item.GetValue<string>("Ten").Contains(tuKhoa.ToUpper()) 
                            && item.GetValue<string>("Tac_gia").ToUpper().Contains(tacGia.ToUpper()))
                        dsach.Add(item.Id, item.GetValue<string>("Ten"));
                }
                isTG = true;
                isName = true;
            }
            if (theLoai.ToUpper() == "KHÔNG")
            {
                theLoai = "";
            }
            if (danhGia != -1 && string.IsNullOrEmpty(theLoai) && tinhTrang == -1)
            {
                query = query.WhereLessThanOrEqualTo("Danh_gia_Tb", danhGia);
            } 
            else if (danhGia == -1 && string.IsNullOrEmpty(theLoai) && tinhTrang != -1)
            {
                query = query.WhereEqualTo("Trang_thai", tinhTrang);
            }
            else if (danhGia == -1 && !string.IsNullOrEmpty(theLoai) && tinhTrang == -1)
            {
                query = query.WhereArrayContains("The_loai", theLoai);
            }
            else if (danhGia != -1 && string.IsNullOrEmpty(theLoai) && tinhTrang != -1)
            {
                query = query.WhereLessThanOrEqualTo("Danh_gia_Tb", danhGia)
                            .WhereEqualTo("Trang_thai", tinhTrang);
            }
            else if (danhGia == -1 && !string.IsNullOrEmpty(theLoai) && tinhTrang != -1) 
            {
                query = query.WhereArrayContains("The_loai", theLoai)
                            .WhereEqualTo("Trang_thai", tinhTrang);
            }
            else if (danhGia != -1 && !string.IsNullOrEmpty(theLoai) && tinhTrang == -1) 
            {
                query = query.WhereLessThanOrEqualTo("Danh_gia_Tb", danhGia)
                            .WhereEqualTo("Trang_thai", tinhTrang);
            }
            else if (danhGia != -1 && !string.IsNullOrEmpty(theLoai) && tinhTrang != -1)
            {
                query = query.WhereLessThanOrEqualTo("Danh_gia_Tb", danhGia)
                            .WhereEqualTo("Trang_thai", tinhTrang)
                            .WhereArrayContains("The_loai", theLoai);
            }

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            foreach (var item in dsach)
            {
                    result.Add(item.Key);   
            }

            /*foreach (var item in dsach2)
            {
                    if (isTG && !result.Contains(item.Key))
                        result.Add(item.Key);
            }*/

            List<string> idsn = new List<string>();
            foreach (var i in snapshot.Documents) idsn.Add(i.Id);

            List<string> result1 = new List<string>();

            foreach (var j in result)
            {
                if(idsn.Contains(j) && !result1.Contains(j)) { 
                    result1.Add(j);
                }
            }

            /*foreach (DocumentSnapshot document in snapshot.Documents)
            {
                bool allc = true;
                if (result.Contains(document.Id))
                {
                    if (isDG && document.GetValue<double>("Danh_gia_Tb") > danhGia)
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
            }*/
            return result1;
        }
    }
}
