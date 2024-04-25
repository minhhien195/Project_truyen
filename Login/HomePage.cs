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
    }
}
