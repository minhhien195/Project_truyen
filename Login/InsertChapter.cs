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
using Firebase.Auth;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.WebExtension;
using FireSharp.Interfaces;
using FireSharp.Config;


namespace Login
{
    public partial class InsertChapter : Form
    {
        UserCredential user;
        private Trang_chu tc;
        public InsertChapter(UserCredential usercredials, Trang_chu tc)
        {
            InitializeComponent();
            this.user = usercredials;
            this.tc = tc;
        }
        string text = "";
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
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
                            text = sr.ReadToEnd();
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
                    text = ReadDocx(dialog.FileName);
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
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse re = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Truyen_dang");

            if (re.Body == "null")
            {
                MessageBox.Show("Bạn không thể thêm chương mới khi không có truyện đã đăng");
                this.Close();
            }
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
                var dict1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(re.Body);
                bool kt = false;
                foreach (var j in dict1)
                {
                    if (txtTruyen.Text == j.Value.ToString())
                    {
                        kt = true;
                        break;
                    }
                    else
                    {
                        kt = false;
                    }

                }
                if (kt == true)
                {
                    FirestoreDb db = FirestoreDb.Create("healtruyen");
                    CollectionReference truyen = db.Collection("Truyen");
                    string tentruyen = txtTruyen.Text.ToUpper();
                    Google.Cloud.Firestore.Query q = truyen.WhereEqualTo("Ten", tentruyen);
                    QuerySnapshot snapshots = await q.GetSnapshotAsync();

                    string id = "";
                    id = snapshots.Documents[0].Id;
                    CollectionReference collectionReference = truyen.Document(id).Collection("Chuong");
                    QuerySnapshot snapshots1 = await collectionReference.GetSnapshotAsync();
                    int value = snapshots1.Documents.Count;


                    if (Convert.ToInt32(txtChuong.Text) > value)
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


                            if (snapshots.Documents.Count > 0)
                            {

                                Dictionary<string, object> updates = new Dictionary<string, object>
                                {
                                    { "So_chuong", value + 1 }
                                };
                                DocumentReference doc = truyen.Document(id);
                                await doc.UpdateAsync(updates);
                                Them_Lay_thongbao tb = new Them_Lay_thongbao();
                                await tb.Them_thongbao_album(id, txtChuong.Text, true);
                            }
                            txtChuong.Text = "";
                            txtTen.Text = "";
                            txtTruyen.Text = "";
                        }
                    }
                    else
                    {
                        Interact.editChap(txtTruyen.Text, txtChuong.Text, "Noi_dung", text, txtTen.Text);
                        DialogResult result = MessageBox.Show($"Đã cập nhật chương {txtChuong.Text}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            ptrTruyen.Visible = false;
                            lbT.Visible = false;
                            ptrSochuong.Visible = false;
                            lbSC.Visible = false;
                            ptrTenchuong.Visible = false;
                            lbTC.Visible = false;
                            ibtninsertDoc.Text = "Thêm chương(.doc, .docx, .txt)";

                            Them_Lay_thongbao tb = new Them_Lay_thongbao();
                            await tb.Them_thongbao_album(id, txtChuong.Text, false);

                            txtChuong.Text = "";
                            txtTen.Text = "";
                            txtTruyen.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng tên truyện của bạn");
                    this.Close();
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
            tc.change_color();
            tc.openChildForm(new Duyet_Truyen(user));
        }

        private async void InsertChapter_Load(object sender, EventArgs e)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse re = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Truyen_dang");

            if (re.Body == "null")
            {
                MessageBox.Show("Bạn không thể thêm chương mới khi không có truyện đã đăng");
                this.Close();
            }
        }
    }
}
