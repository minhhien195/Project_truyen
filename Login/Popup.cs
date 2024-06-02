using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Popup : Form
    {
        public Popup()
        {
            InitializeComponent();
        }
        string nameNovel;
        private void ibtnDanhgia_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            ibtnDanhgia.IconColor = Color.Black;
            try
            {
                using (Danhgia danhgia = new Danhgia())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.Show();

                    danhgia.Owner = formBackground;
                    danhgia.ShowDialog();

                    formBackground.Dispose();
                }
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private async void ibtnDecu_Click(object sender, EventArgs e)
        {
            if (ibtnDecu.IconColor == Color.Black)
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
                DocumentReference collectionRef = db.Collection("Truyen").Document(id);
                DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();

                int Decu = snapshot.GetValue<int>("De_cu");
                Decu++;

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "De_cu", Decu },
                };
                DocumentReference doc = truyen.Document(id);
                await doc.UpdateAsync(updates);
            }
            else
            {
                MessageBox.Show("Lỗi! Không thể đề cử.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void ibtnDanhgia_MouseHover(object sender, EventArgs e)
        {
            ibtnDanhgia.IconColor = Color.IndianRed;
        }

        private void ibtnDanhgia_MouseClick(object sender, MouseEventArgs e)
        {
            ibtnDanhgia.IconColor = Color.IndianRed;
        }

        private void ibtnDecu_MouseClick(object sender, MouseEventArgs e)
        {
            ibtnDecu.IconColor = Color.IndianRed;
        }

        private void ibtnDecu_MouseHover(object sender, EventArgs e)
        {
            ibtnDecu.IconColor = Color.IndianRed;
        }
        public Popup(string nameNovel)
        {
            this.nameNovel = nameNovel;
        }
        private void Popup_Load(object sender, EventArgs e)
        {

        }
    }
}
