using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Novel;

namespace Login
{
    public partial class InsertChapter : Form
    {
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
                text = System.IO.File.ReadAllText(dialog.FileName);
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
                    txtChuong.Text = "";
                    txtTen.Text = "";
                    txtTruyen.Text = "";
                    ibtninsertDoc.Text = "Thêm chương(.doc, .docx, .txt)";

                    FirestoreDb db = FirestoreDb.Create("healtruyen");
                    CollectionReference truyen = db.Collection("Truyen");
                    string tentruyen = txtTen.Text.ToUpper();
                    Query q = truyen.WhereEqualTo("Ten", tentruyen);
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
                    }
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
