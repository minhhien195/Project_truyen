using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace Readinghistory
{
    public partial class Readinghistory : Form
    {
/*        const Config = new FirebaseConfig
        {
            AuthKey = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            RealtimeDatabaseURL = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/",
        };*/
        IFirebaseClient Client;
        public Readinghistory()
        {
            InitializeComponent();
        }

        private void Readinghistory_Load(object sender, EventArgs e)
        {
           // Client = new FireSharp.FirebaseClient(Config);

            if (Client != null)
            {
                MessageBox.Show("Connected successfully!");
            }
        }
    }
}
