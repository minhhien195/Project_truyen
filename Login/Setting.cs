using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static Login.Signup;

namespace Login
{
    public partial class Setting : Form
    {
        UserCredential user;
        FirebaseAuthClient client;
        IFirebaseClient ifclient;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public Setting(UserCredential userCredential, FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            ifclient = new FireSharp.FirebaseClient(config);
            this.user = userCredential;
            this.client = firebaseAuthClient;

        }
        private bool isNullCur = false, isNullMK = false, isNULLXNMK = false, lengthMK = false, changePW = false;
        private string RemoveDiacritics(string text)
        {
            string normalizedText = text.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            foreach (char c in normalizedText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            tbnewpw.PasswordChar = '*';
            string inputText = tbnewpw.Text;

            // Chuyển đổi văn bản thành mã ký tự không dấu
            string normalizedText = RemoveDiacritics(inputText);

            // Tắt UTF-8 và cập nhật văn bản trong TextBox
            byte[] asciiBytes = Encoding.ASCII.GetBytes(normalizedText);
            string asciiText = Encoding.ASCII.GetString(asciiBytes);
            tbnewpw.Text = asciiText;

            // Đặt con trỏ văn bản lại vị trí cuối cùng
            tbnewpw.SelectionStart = tbnewpw.Text.Length;
        }

        private void txtXNMK_TextChanged(object sender, EventArgs e)
        {
            tbrenewpw.PasswordChar = '*';
            string inputText = tbrenewpw.Text;

            // Chuyển đổi văn bản thành mã ký tự không dấu
            string normalizedText = RemoveDiacritics(inputText);

            // Tắt UTF-8 và cập nhật văn bản trong TextBox
            byte[] asciiBytes = Encoding.ASCII.GetBytes(normalizedText);
            string asciiText = Encoding.ASCII.GetString(asciiBytes);
            tbrenewpw.Text = asciiText;

            // Đặt con trỏ văn bản lại vị trí cuối cùng
            tbrenewpw.SelectionStart = tbrenewpw.Text.Length;
            if (tbrenewpw.Text != tbnewpw.Text)
            {
                ptrNotsame.Visible = true;
                lbNotsame.Visible = true;
            }
            else
            {
                changePW = true;
                ptrNotsame.Visible = false;
                lbNotsame.Visible = false;
            }

        }

       


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tbcurpw_TextChanged(object sender, EventArgs e)
        {
            tbcurpw.PasswordChar = '*';
            string inputText = tbcurpw.Text;

            // Chuyển đổi văn bản thành mã ký tự không dấu
            string normalizedText = RemoveDiacritics(inputText);

            // Tắt UTF-8 và cập nhật văn bản trong TextBox
            byte[] asciiBytes = Encoding.ASCII.GetBytes(normalizedText);
            string asciiText = Encoding.ASCII.GetString(asciiBytes);
            tbcurpw.Text = asciiText;

            // Đặt con trỏ văn bản lại vị trí cuối cùng
            tbcurpw.SelectionStart = tbcurpw.Text.Length;
        }
        static async Task ChangeUserEmail(string uid, string newEmail)
        {
            try
            {
                // Lấy thông tin người dùng hiện tại
                UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

                if (user == null)
                {
                    Console.WriteLine("Không tìm thấy người dùng với UID đã cho.");
                    return;
                }

                // Cập nhật email
                UserRecordArgs args = new UserRecordArgs()
                {
                    Uid = uid,
                    Email = newEmail
                };

                // Cập nhật thông tin người dùng
                UserRecord updatedUser = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);

                Console.WriteLine($"Email mới: {updatedUser.Email}");
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                Console.WriteLine($"Lỗi FirebaseAuth: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khác: {ex.Message}");
            }
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
            var client1 = new FirebaseAuthClient(config);
            if (string.IsNullOrEmpty(tbcurpw.Text))
            {
                isNullMK = true;
            }
            else
            {
                isNullMK = false;
            }
            if (string.IsNullOrEmpty(tbnewpw.Text))
            {
                isNullMK = true;
            }
            else
            {
                isNullMK = false;
            }
            if (string.IsNullOrEmpty(tbrenewpw.Text))
            {
                isNULLXNMK = true;
            }
            else
            {
                isNULLXNMK = false;
            }
            if (changePW == true)
            {
                try
                {

                    var token = await client.SignInWithEmailAndPasswordAsync(user.User.Info.Email, tbcurpw.Text);
                    if (tbnewpw.Text.Length < 6)
                    {
                        lengthMK = true;
                    }
                    else
                    {
                        lengthMK = false;
                    }
                    if (ptrNotsame.Visible == false && lbNotsame.Visible == false && isNullCur == false 
                        && isNullMK == false && isNULLXNMK == false && lengthMK == false)
                    {    
                    
                            await user.User.ChangePasswordAsync(tbnewpw.Text);
                            MessageBox.Show("Thay đổi mật khẩu thành công");
                    }
                    else if (lengthMK == true)
                    {
                        MessageBox.Show("Mật khẩu phải lớn hơn 5 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa thể đăng ký", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nhập mật khẩu hiện tại sai!Hãy nhập lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
