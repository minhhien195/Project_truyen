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
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Auth.Providers;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using FontAwesome.Sharp;
using thongbao;

namespace Login
{
    public partial class Xuli_vipham : Form
    {
        public class Vipham
        {
            public string Id_bi_tocao { get; set; }
            public string Noi_dung_to_cao { get; set; }
            public string So_lan_canh_cao { get; set; }
        }
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        static FirebaseAuthConfig config1 = new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
            AuthDomain = "healtruyen.firebaseapp.com",
            Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                new EmailProvider()
                }
        };

        FirebaseAuthClient client1 = new FirebaseAuthClient(config1);

        public Xuli_vipham()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
        }

        private async void Xuli_vipham_Load(object sender, EventArgs e) 
        {
            FirebaseResponse res4 = await client.GetAsync("Vi_pham/");
            if (res4.Body != "null")
            {
                var dict1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(res4.Body);
                int dem1 = 0;
                foreach (var j in dict1)
                {
                    dem1 = Convert.ToInt32(j.Key);
                }
                for (int i = 0; i < dem1; i++)
                {
                    string report = "";
                    for (int j = 0; j < 3 - dem1.ToString().Length; j++)
                    {
                        report += "0";
                    }
                    report += dem1.ToString();
                    FirebaseResponse res = await client.GetAsync("Vi_pham/" + report);

                    Vipham vipham = res.ResultAs<Vipham>();
                    if (vipham is null) continue;


                    TableLayoutPanel tb1 = new TableLayoutPanel();
                    panel1.Controls.Add(tb1);
                    tb1.RowCount = 2;
                    tb1.ColumnCount = 1;
                    tb1.AutoSize = true;
                    tb1.RowStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tb1.RowStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tb1.Dock = DockStyle.Top;

                    RichTextBox content = new RichTextBox();
                    content.Text = vipham.Noi_dung_to_cao;
                    tb1.Controls.Add(content, 0, 0);
                    content.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    content.Dock = DockStyle.Fill;
                    content.AutoSize = true;
                    content.AllowDrop = true;
                    content.BorderStyle = BorderStyle.None;
                    content.BackColor = Color.FromArgb(220, 247, 253);

                    TableLayoutPanel tb = new TableLayoutPanel();
                    tb1.Controls.Add(tb, 0, 1);
                    tb.RowCount = 1;
                    tb.ColumnCount = 3;
                    tb.AutoSize = true;
                    tb.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
                    tb.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                    tb.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                    tb.Dock = DockStyle.Fill;
                    tb.BringToFront();


                    IconButton canhcao = new IconButton();
                    tb.Controls.Add(content, 1, 0);
                    canhcao.Dock = DockStyle.Fill;
                    canhcao.Text = "Cảnh cáo";
                    canhcao.AutoSize = true;
                    canhcao.Font = new Font("League Spartan", 12, FontStyle.Regular);
                    canhcao.IconChar = IconChar.Flag;
                    canhcao.IconSize = 30;
                    canhcao.TextAlign = ContentAlignment.MiddleRight;
                    canhcao.TextImageRelation = TextImageRelation.ImageBeforeText;
                    canhcao.Click += async (s, ev) =>
                    {
                        if (Convert.ToInt32(vipham.So_lan_canh_cao) <= 3)
                        {
                            Them_Lay_thongbao tbao = new Them_Lay_thongbao();
                            await tbao.Them_thongbao_canhcao(vipham.Id_bi_tocao, Convert.ToInt32(vipham.So_lan_canh_cao));
                            await client.UpdateAsync("Vi_pham/" + report + "/So_lan_canh_cao", (Convert.ToInt32(vipham.So_lan_canh_cao) + 1).ToString());
                            MessageBox.Show("Cảnh cáo người dùng thành công");
                        }
                        else
                        {
                            FirebaseResponse r = await client.GetAsync("Nguoi_dung/" + vipham.Id_bi_tocao + "/disable");
                            bool disable = r.ResultAs<bool>();
                            if (!disable)
                            {
                                await client.SetAsync("Nguoi_dung/" + vipham.Id_bi_tocao + "/disable", true);
                            }
                        }
                        //await client.DeleteAsync("Vi_pham/" + report);

                    };

                    IconButton Ban = new IconButton();
                    tb.Controls.Add(Ban, 2, 0);
                    Ban.Dock = DockStyle.Fill;
                    Ban.Text = "Vô hiệu hóa";
                    Ban.Font = new Font("League Spartan", 12, FontStyle.Regular);
                    Ban.IconChar = IconChar.Flag;
                    Ban.IconSize = 30;
                    Ban.AutoSize = true;
                    Ban.TextAlign = ContentAlignment.MiddleRight;
                    Ban.TextImageRelation = TextImageRelation.ImageBeforeText;
                    Ban.Click += async (s, ev) =>
                    {
                        FirebaseResponse r = await client.GetAsync("Nguoi_dung/" + vipham.Id_bi_tocao + "/disable");
                        bool disable = r.ResultAs<bool>();
                        if (!disable)
                        {
                            await client.SetAsync("Nguoi_dung/" + vipham.Id_bi_tocao + "/disable", true);

                        }
                        //await client.DeleteAsync("Vi_pham/" + report);
                    };



                }
                
            } else
            {
                Label label = new Label();
                label.Text = "Không có vi phạm nào cần xử lý.";
                label.Font = new Font("League Spartan", 16, FontStyle.Regular);
                label.AutoSize = true;
                this.Controls.Add(label);
                label.Dock = DockStyle.Top;
            }
        }

    }
}
