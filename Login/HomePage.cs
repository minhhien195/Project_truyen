using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using Firebase.Auth;

namespace Login
{
    public partial class HomePage : Form
    {
        UserCredential user;
        FirebaseAuthClient client;
        public HomePage(UserCredential userCredential, FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            this.user = userCredential;
            this.client = firebaseAuthClient;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            label2.Text = user.User.Info.DisplayName;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            client.SignOut();
            this.Hide();
            Login login = new Login();
            login.Show();

        }
<<<<<<< HEAD

        private async void loadava_Click(object sender, EventArgs e)
        {
            string ID = user.User.Info.Uid;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                string extend = System.IO.Path.GetExtension(fileName);
                if (extend == ".png" || extend == ".jpg")
                {
                    Bitmap bitmap = new Bitmap(openFileDialog.FileName);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageBytes1 = memoryStream.ToArray();

                        // Convert byte array to base64 string
                        string base64String1 = Convert.ToBase64String(imageBytes1);
                        // Set the data in Firebase
                        var setResponse = await ifclient.SetAsync("Nguoi_dung/" + ID + "/Anh_dai_dien", base64String1);
                        MessageBox.Show("Avatar uploaded successfully!");

                    }
                    FirebaseResponse response = await ifclient.GetAsync("Nguoi_dung/" + ID + "/Anh_dai_dien");
                    string base64String2 = response.ResultAs<string>();
                    byte[] imageBytes2 = Convert.FromBase64String(base64String2);

                    using (MemoryStream memoryStream = new MemoryStream(imageBytes2))
                    {
                        bitmap = new Bitmap(memoryStream);
                        pictureBox1.Image = bitmap;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else MessageBox.Show("Error file","Use file have .png or .jpg",MessageBoxButtons.OK, MessageBoxIcon.Warning);

                

            }
            
        }
=======
>>>>>>> f17848eb79079e9a6ff1def60b6539145a933b16
    }
}
