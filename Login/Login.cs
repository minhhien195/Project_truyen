using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Firebase.Auth.Providers;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;

namespace Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void Login_Load(object sender, EventArgs e)
        {
            //Bo góc panel
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath path1 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 20; // Điều chỉnh giá trị này để thay đổi bán kính bo góc

            // Thêm hình dạng bo góc vào GraphicsPath
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(panel1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(panel1.Width - cornerRadius, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(0, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();

            path1.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path1.AddArc(textBox1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path1.AddArc(textBox1.Width - cornerRadius, textBox1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path1.AddArc(0, textBox1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path1.CloseAllFigures();

            path2.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path2.AddArc(textBox2.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path2.AddArc(textBox2.Width - cornerRadius, textBox2.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path2.AddArc(0, textBox2.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path2.CloseAllFigures();


            // Thiết lập Region của Panel bằng GraphicsPath
            panel1.Region = new Region(path);
            textBox1.Region = new Region(path1);
            textBox2.Region = new Region(path2);

            //Căn giữa panel
            panel1.Location = new Point(
                (this.Width - panel1.Width) / 2, // Tính toán vị trí theo trục X
                (this.Height - panel1.Height) / 2 // Tính toán vị trí theo trục Y
            );
            panel1.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor

            label1.Location = new Point(
                (panel1.Width - label1.Width) / 2, // Tính toán vị trí theo trục X
                13
            );
            label1.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //string pathToFirebaseConfig = @"C:\TLHT\HK2_2023-2024\LapTrinhMangCanBan\HealTruyen\healtruyen-firebase-adminsdk-ww2hz-70b28e08d2.json";

            // Khởi tạo FirebaseApp
            /*FirebaseApp app = FirebaseApp.Create(new AppOptions()
            {
                Credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile(pathToFirebaseConfig)
            });*/

            // Khởi tạo FirebaseAuth
            //FirebaseAuth auth = FirebaseAuth.GetAuth(app);

            //config Firebase Authentication
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                AuthDomain = "healtruyen.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                new EmailProvider()
                }
            };

            var client = new FirebaseAuthClient(config);

            try
            {
                UserCredential userCredential = await SignIn( client, textBox1.Text, textBox2.Text);
                if (userCredential != null)
                {
                    MessageBox.Show("Sigin succeed!");
                }
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                //MessageBox.Show("error");
                MessageBox.Show(ex.Reason.ToString());
                return;
            }
        }


        static async Task<UserCredential> SignIn( FirebaseAuthClient client, string Email, string Password)
        {
            string email = Email;

            //check user exists
            /*var result = await app.GetUserByEmailAsync(email);
            MessageBox.Show(result.Uid);*/
            var result = await client.FetchSignInMethodsForEmailAsync(email);

            if (result.UserExists)
            {
                var password = Password;
                var credential = EmailProvider.GetCredential(email, password);
                var emailUser = await client.SignInWithCredentialAsync(credential);
                /*while (emailUser.User.Uid == null)
                {
                    credential = EmailProvider.GetCredential(email, password);
                    emailUser = await client.SignInWithCredentialAsync(credential);
                }*/
                if (emailUser.User.Uid == null)
                {
                    //MessageBox.Show("Password wrong");
                    return null;
                }
                return emailUser;
            }
            else
            {
                return null;
                
            }

        }
    }
}
