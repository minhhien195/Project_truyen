using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using FirebaseAdmin;

using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace Novel
{

    public static class Interact
    {
        public class Chapter
        {
            [FirestoreProperty("ID_Truyen")]
            public string idBook { get; set; }
            [FirestoreProperty("Noi_dung")]
            public string Content { get; set; }
            [FirestoreProperty("TG_dangtai")]
            public Timestamp Times { get; set; }
            [FirestoreProperty("Tieu_de")]
            public string Title { get; set; }
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
        public async static Task<Chapter> getNovel(string nameNovel, string numChapter)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference collectionReference = db.Collection("Truyen");
            nameNovel = nameNovel.ToUpper();
            Query q = collectionReference.WhereEqualTo("Ten", nameNovel);
            QuerySnapshot qs = await q.GetSnapshotAsync();
            if (qs.Documents.Count == 0)
            {
                return null;
            }
            string chapId = "";
            int cnt = 4 - numChapter.Length;
            for (int i = 0; i < cnt; i++)
            {
                chapId += "0";
            }
            chapId += numChapter;
            DocumentReference collectionRef = db.Collection("Truyen").Document(qs.Documents[0].Id).Collection("Chuong").Document(chapId);
            DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {

                Chapter novel = snapshot.ConvertTo<Chapter>();
                return novel;
            }
            else
            {
                return null;
            }
        }

        public static async void pushChapter(string nameNovel, string numChapter, string title, string content)
        {

            FirestoreDb db = FirestoreDb.Create("healtruyen");
            CollectionReference collectionRef = db.Collection("Truyen");
            nameNovel = nameNovel.ToUpper();
            Query q = collectionRef.WhereEqualTo("Ten", nameNovel);
            QuerySnapshot snapshots = await q.GetSnapshotAsync();
            int numChap = Convert.ToInt32(numChapter);
            if (snapshots.Count == 0)
            {
                return;
            }
            var novelId = snapshots[0].Id;
            CollectionReference chapterRef = collectionRef.Document(novelId).Collection("Chuong");
            QuerySnapshot documentQs = await chapterRef.GetSnapshotAsync();
            foreach (var item in documentQs)
            {
                if (item.Exists)
                {
                    if (Convert.ToInt32(item.Id) == numChap)
                    {
                        return;
                    }
                }
            }

            foreach (DocumentSnapshot doc in snapshots.Documents)
            {
                if (doc.Exists)
                {
                    var id = doc.Id;
                    CollectionReference chap = collectionRef.Document(id).Collection("Chuong");
                    if (!title.StartsWith("Chương") || !title.StartsWith("CHƯƠNG"))
                    {
                        title = "Chương " + numChapter + ": " + title;
                    }
                    var dataAdd = new
                    {
                        ID_Truyen = id,
                        Noi_dung = content,
                        TG_dangtai = System.DateTime.UtcNow,
                        Tieu_de = title
                    };
                    string chapID = "";
                    int cnt = 4 - numChapter.Length;
                    for (int i = 0; i < cnt; i++)
                    {
                        chapID += "0";
                    }
                    chapID += numChapter;

                    DocumentReference chapUp = chap.Document(chapID);
                    await chapUp.CreateAsync(dataAdd);
                }
            }

        }

        public static async Task<string> createNovel(string bia_sach, string so_luong_chuong, string tac_gia, string ten_truyen, string[] the_loai, string tom_tat, string dbString)
        {
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            Novel novel = new Novel()
            {
                coverImg = bia_sach,
                cntChapter = Convert.ToInt32(so_luong_chuong),
                comment = 0,
                numRating = 0,
                avgRating = 0,
                recommend = 0,
                like = 0,
                numRead = 0,
                times = Timestamp.FromDateTime(DateTime.UtcNow),
                author = tac_gia,
                nameNovel = ten_truyen,
                type = the_loai,
                description = tom_tat,
                status = 1
            };
            CollectionReference truyen = db.Collection(dbString);
            QuerySnapshot qs = await truyen.GetSnapshotAsync();
            int order = qs.Count + 1;
            string novelId = "";
            for (int i = 0; i < 3 - order.ToString().Length; i++)
            {
                novelId += "0";
            }
            novelId += order.ToString();
            DocumentReference novelUp = truyen.Document(novelId);
            await novelUp.CreateAsync(novel);
            return novelId;
        }

        public async static void editNovel(string nameNovel, string field, string value)
        {
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            CollectionReference truyen = db.Collection("Truyen");
            nameNovel = nameNovel.ToUpper();
            Query q = truyen.WhereEqualTo("Ten", nameNovel);
            QuerySnapshot snapshots = await q.GetSnapshotAsync();
            string id = "";
            if (snapshots.Documents.Count > 0)
            {
                id = snapshots.Documents[0].Id;
            }
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { field, value }
            };
            DocumentReference doc = truyen.Document(id);
            await doc.UpdateAsync(updates);
        }

        public async static void editChap(string nameNovel, string numChap, string field, string value)
        {
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            CollectionReference truyen = db.Collection("Truyen");
            nameNovel = nameNovel.ToUpper();

            // Lấy id của truyện bằng tên
            Query q = truyen.WhereEqualTo("Ten", nameNovel);
            QuerySnapshot snapshots = await q.GetSnapshotAsync();
            string id = "";
            if (snapshots.Documents.Count > 0)
            {
                id = snapshots.Documents[0].Id;
            }

            //Lấy id của chương
            string chapID = "";
            int cnt = 4 - numChap.Length;
            for (int i = 0; i < cnt; i++)
            {
                chapID += "0";
            }
            chapID += numChap;

            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { field, value }
            };
            DocumentReference doc = truyen.Document(id).Collection("Chuong").Document(chapID);
            await doc.UpdateAsync(updates);
        }

        public async static void deleteNovel(string nameNovel, string dbString)
        {
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            CollectionReference truyen = db.Collection(dbString);
            nameNovel = nameNovel.ToUpper();
            Query q = truyen.WhereEqualTo("Ten", nameNovel);
            QuerySnapshot snapshots = await q.GetSnapshotAsync();
            string id = "";
            if (snapshots.Documents.Count > 0)
            {
                id = snapshots.Documents[0].Id;
            }

            DocumentReference doc = truyen.Document(id);
            await doc.DeleteAsync();
        }

        public async static void deleteChap(string nameNovel, string numChap)
        {
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            CollectionReference truyen = db.Collection("Truyen");
            nameNovel = nameNovel.ToUpper();

            // Lấy id của truyện bằng tên
            Query q = truyen.WhereEqualTo("Ten", nameNovel);
            QuerySnapshot snapshots = await q.GetSnapshotAsync();
            string id = "";
            if (snapshots.Documents.Count > 0)
            {
                id = snapshots.Documents[0].Id;
            }

            //Lấy id của chương
            string chapID = "";
            int cnt = 4 - numChap.Length;
            for (int i = 0; i < cnt; i++)
            {
                chapID += "0";
            }
            chapID += numChap;

            DocumentReference doc = truyen.Document(id).Collection("Chuong").Document(chapID);
            await doc.DeleteAsync();
        }
        public static void scrapingNovel(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            HtmlAgilityPack.HtmlDocument doc = htmlWeb.Load("https://metruyencv.com/truyen/buc-ta-trong-sinh-dung-khong/chuong-1");
            HtmlNode chapterContent = doc.DocumentNode.SelectSingleNode("//*[@id=\"js-read__body\"]");
            //string title_chapter = Encoding.UTF8.GetStringEncoding.Default.GetBytes(chapterContent.SelectSingleNode("//*[@id=\"js-read__body\"]/div[2]").InnerText);
            //string content_chapter = Encoding.UTF8.GetStringEncoding.Default.GetBytes(chapterContent.SelectSingleNode("//*[@id=\"js-read__body\"]/div[2]").InnerText););
            var title = chapterContent.SelectSingleNode("//*[@id=\"js-read__body\"]/div[2]").InnerText;
            var text = chapterContent.SelectSingleNode("//*[@id=\"article\"]").InnerHtml;
            var subStrRemove = "<div class=\"pt-3 text-center\" style=\"margin-right: " +
                "-1rem;\"><div class=\"mb-1 fz-13\"><small class=\"text-muted\"><small>&mdash; " +
                "QUẢNG CÁO &mdash;</small></small></div><div class=\"my-1\"></div></div>";
            text = text.Replace("<br><br>", "\n\n");
            text = text.Replace(subStrRemove, "");
        }
    }
}
