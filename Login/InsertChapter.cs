using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Novel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FireSharp.Response;
using Newtonsoft.Json;
using thongbao;


namespace Login
{
    public partial class InsertChapter : Form
    {

        public class Novel
        {
            [FirestoreProperty("Anh")]
            public string coverImg { get; set; }
            [FirestoreProperty("Binh_luan")]
            public int comment { get; set; }
            [FirestoreProperty("Danh_gia")]
            public int numRating { get; set; }
            [FirestoreProperty("Diem_danhgia")]
            public int totalRating { get; set; }
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

        public InsertChapter()
        {
            InitializeComponent();
        }
        string text = "";
        private void ibtninsertDoc_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Word Files (*.doc; *.docx)|*.doc;*.docx|Text Files (*.txt)|*.txt";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ibtninsertDoc.Text = dialog.FileName;
                if(dialog.FileName.ToString().EndsWith("txt"))
                {
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string text = sr.ReadToEnd();
                            if (text == "")
                            {
                                ptrThemchuongmoi.Visible = true;
                                lbTCM.Visible = true;
                            }
                            else
                            {
                                ptrThemchuongmoi.Visible = false;
                                lbTCM.Visible = false;
                            }
                        }
                    }
                } else if(dialog.FileName.EndsWith("docx") ||  dialog.FileName.EndsWith("doc"))
                {
                    string text = ReadDocx(dialog.FileName);
                    if (text == "")
                    {
                        ptrThemchuongmoi.Visible = true;
                        lbTCM.Visible = true;
                    }
                    else
                    {
                        ptrThemchuongmoi.Visible = false;
                        lbTCM.Visible = false;
                    }
                }                
            }
        }

        string ReadDocx(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
            {
                Body body = wordDoc.MainDocumentPart.Document.Body;

                foreach (Paragraph paragraph in body.Descendants<Paragraph>())
                {
                    sb.AppendLine(paragraph.InnerText);
                }
            }

            return sb.ToString();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (ibtninsertDoc.Text == "Thêm chương(.doc, .docx, .txt)" || ibtninsertDoc.Text == "")
            {
                ptrThemchuongmoi.Visible = true;
                lbTCM.Visible = true;
            }
            if (txtTruyen.Text == "")
            {
                ptrTruyen.Visible = true;
                lbT.Visible = true;
            }
            if (txtTen.Text == "")
            {
                ptrTenchuong.Visible = true;
                lbTC.Visible = true;
            }
            if (txtChuong.Text == "")
            {
                ptrSochuong.Visible = true;
                lbSC.Visible = true;
            }
            if (lbTCM.Visible == false && lbT.Visible == false && lbTC.Visible == false && lbSC.Visible == false)
            {
                Interact.pushChapter(txtTruyen.Text, txtChuong.Text, txtTen.Text, text);
                DialogResult result = MessageBox.Show("Đã thêm chương mới!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    ptrTruyen.Visible = false;
                    lbT.Visible = false;
                    ptrSochuong.Visible = false;
                    lbSC.Visible = false;
                    ptrTenchuong.Visible = false;
                    lbTC.Visible = false;
                    ibtninsertDoc.Text = "Thêm chương(.doc, .docx, .txt)";

                    FirestoreDb db = FirestoreDb.Create("healtruyen");
                    CollectionReference truyen = db.Collection("Truyen");
                    string tentruyen = txtTruyen.Text.ToUpper();
                    Google.Cloud.Firestore.Query q = truyen.WhereEqualTo("Ten", tentruyen);
                    QuerySnapshot snapshots = await q.GetSnapshotAsync();
                    string id = "";
                    if (snapshots.Documents.Count > 0)
                    {
                        id = snapshots.Documents[0].Id;
                        CollectionReference collectionReference = truyen.Document(id).Collection("Chuong");
                        QuerySnapshot snapshots1 = await collectionReference.GetSnapshotAsync();
                        int value = snapshots1.Documents.Count;
                        Dictionary<string, object> updates = new Dictionary<string, object>
                        {
                            { "So_chuong", value }
                        };
                        DocumentReference doc = truyen.Document(id);
                        await doc.UpdateAsync(updates);
                        Them_Lay_thongbao tb = new Them_Lay_thongbao();
                        tb.Them_thongbao_album(id);
                    }
                    txtChuong.Text = "";
                    txtTen.Text = "";
                    txtTruyen.Text = "";
                }
            }

        }

        private void txtTruyen_TextChanged(object sender, EventArgs e)
        {
            if (lbT.Visible)
            {
                ptrTruyen.Visible = false;
                lbT.Visible = false;
            }
        }

        private void txtChuong_TextChanged(object sender, EventArgs e)
        {
            if (lbSC.Visible)
            {
                ptrSochuong.Visible = false;
                lbSC.Visible = false;
            }
        }

        private void txtTen_TextChanged(object sender, EventArgs e)
        {
            if (lbTC.Visible)
            {
                ptrTenchuong.Visible = false;
                lbTC.Visible = false;
            }
        }

        private void ibtnDangtruyen_Click(object sender, EventArgs e)
        {
            this.Close();
            InsertNovel insertNovel = new InsertNovel();
            insertNovel.Show();
        }
    }
}
