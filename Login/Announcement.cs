using Firebase.Auth.Providers;
using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebaseAdmin.Auth;
using thongbao;
using FontAwesome.Sharp;
using System.Globalization;

namespace Login
{
    public partial class Announcement : Form
    {

        UserCredential user;
        public Announcement(UserCredential userCredential)
        {
            InitializeComponent();
            this.user = userCredential;
        }


        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void Announcement_Load(object sender, EventArgs e)
        {
            string ID = user.User.Uid;
            Them_Lay_thongbao thongbao = new Them_Lay_thongbao();
            Task<Dictionary<string, Dictionary<string, object>>> laythongbao = thongbao.Lay_thongbao(ID);
            
            var result = await laythongbao;
            int cnt = 0;
            /*MessageBox.Show($"{result.Keys} Lấy thành công");*/
            foreach (var item in result)
            {
                cnt++;
                /*switch (cnt)
                {
                    case 1:  label1.Text = item.Value["Noi_dung"].ToString(); break;
                }*/
                Label tg = Controls.Find("label" + cnt, true).FirstOrDefault() as Label;
                tg.Text = item.Value["Noi_dung"].ToString();
                IconButton iconButton = Controls.Find("iconButton" + cnt, true).FirstOrDefault() as IconButton;
                string datetime = item.Value["TG_gui"].ToString();
                
                DateTime startDateTime = DateTime.Parse(datetime, CultureInfo.InvariantCulture);
                DateTime currentDateTime = DateTime.Now;

                TimeSpan elapsedTime = currentDateTime - startDateTime;
                int elapsedSeconds = (int)elapsedTime.TotalSeconds;
                int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                int elapsedHours = (int)elapsedTime.TotalHours;
                int elapsedDays = (int)elapsedTime.TotalDays;
                int elapsedMonth = elapsedDays / 30;
                int elapsedYear = elapsedMonth / 12;
                if (elapsedYear > 0)
                {
                    iconButton.Text = elapsedYear.ToString() + " năm trước";
                }
                else if (elapsedMonth > 0)
                {
                    iconButton.Text = elapsedMonth.ToString() + " tháng trước";
                }
                else if (elapsedDays > 0)
                {
                    iconButton.Text = elapsedDays.ToString() + " ngày trước";
                }
                else if (elapsedHours > 0)
                {
                    iconButton.Text = elapsedHours.ToString() + " giờ trước";
                }
                else if (elapsedMinutes > 0)
                {
                    iconButton.Text = elapsedMinutes.ToString() + " phút trước";
                }
                else
                {
                    iconButton.Text = elapsedSeconds.ToString() + " giây trước";
                }
                if (cnt == 10) break;
            }
            if (cnt != 10)
            {
                if (cnt < 7) this.AutoScroll = false;
                cnt++;
                for (int i = cnt; i <= 10; i++)
                {
                    Panel panel = Controls.Find("panel" + (i + 1), true).FirstOrDefault() as Panel;
                    panel.Hide();
                }
            }
        }
    }
}
