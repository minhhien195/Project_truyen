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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Globalization;

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
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;

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
            path1.AddArc(txtEmail.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path1.AddArc(txtEmail.Width - cornerRadius, txtEmail.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path1.AddArc(0, txtEmail.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path1.CloseAllFigures();

            path2.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path2.AddArc(txtMK.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path2.AddArc(txtMK.Width - cornerRadius, txtMK.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path2.AddArc(0, txtMK.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path2.CloseAllFigures();


            // Thiết lập Region của Panel bằng GraphicsPath
            panel1.Region = new Region(path);
            txtEmail.Region = new Region(path1);
            txtMK.Region = new Region(path2);

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
                UserCredential userCredential = await SignIn( client, txtEmail.Text, txtMK.Text);
                if (userCredential != null)
                {
                    this.Hide();
                    Trang_chu form = new Trang_chu(userCredential, client, true);
                    form.Show();   

                }
                else
                {
                    MessageBox.Show("Email không tồn tại");
                }
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                //MessageBox.Show("error");
                if (ex.Reason.ToString() == "WrongPassword")
                {
                    ptrWarning1.Visible = true;
                    labelMKsai.Visible = true;
                }
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

        private void hidePwd_Click(object sender, EventArgs e)
        {
            showPwd.BringToFront();
            if (txtMK.PasswordChar == '*')
            {
                txtMK.PasswordChar = '\0';
            }
        }

        private void showPwd_Click(object sender, EventArgs e)
        {
            hidePwd.BringToFront();
            if (txtMK.PasswordChar == '\0')
            {
                txtMK.PasswordChar = '*';
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ptrWarning1.Visible = true;
            labelMKsai.Visible = true;
        }
        

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
           
            var r = new System.Text.RegularExpressions.Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            if (r.IsMatch(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
            else
            {
                ptrWarning.Visible = true;
                lbemailKhonghople.Visible = true;
            }
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            
            if (txtMK.Text.Length < 6)
            {
                ptrWarning1.Visible = true;
                labelMKinvalid.Visible = true;

            }
            else
            {
                ptrWarning1.Visible = false;
                labelMKinvalid.Visible = false;
                labelMKsai.Visible = false;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
        }

        private void txtEmail_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
        }

        private void txtEmail_MouseHover(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự là một trong các ký tự có dấu tiếng Việt
            if (IsVietnameseDiacritic(e.KeyChar))
            {
                // Thay thế ký tự có dấu thành ký tự không dấu
                e.KeyChar = RemoveVietnameseDiacritic(e.KeyChar);
            }
        }

        // Kiểm tra xem ký tự có phải là một ký tự có dấu tiếng Việt hay không
        private bool IsVietnameseDiacritic(char c)
        {
            string diacritics = "àáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđ";

            return diacritics.Contains(c);
        }

        // Xóa dấu của ký tự tiếng Việt
        private char RemoveVietnameseDiacritic(char c)
        {
            string withDiacritic = "àáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđ";
            Dictionary<int, string> test = new Dictionary<int, string>();
            string withoutDiacritic = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyd";

            int index = withDiacritic.IndexOf(c);
            if (index >= 0)
            {
                c = withoutDiacritic[index];
            }

            return c;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forget_Password forget_Password = new Forget_Password();
            this.Hide();
            forget_Password.Show();
            //this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Signup signup = new Signup();
            signup.Show();
        }
    }
}
