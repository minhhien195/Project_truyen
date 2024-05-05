﻿
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlbumTruyen;
using thongbao;
using FireSharp.Config;
using FireSharp.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Auth.Providers;
using Firebase.Auth;
using System.Windows.Documents;
using System.IO;


namespace Login
{
    public partial class AlbumTruyen : Form
    {

        public AlbumTruyen()
        {
            InitializeComponent();

        }

        private void AlbumTruyen_Load(object sender, EventArgs e)
        {

        }
        Dictionary<string, Dictionary<string, object>> albumtruyen = new Dictionary<string, Dictionary<string, object>>();
        List<Dictionary<string, object>> noidung_album = new List<Dictionary<string, object>>();
        List<string> idtruyen = new List<string>();
        Font tentruyen = new Font("League Spartan SemiBold", 12.0f);
        Font chuong = new Font("League Spartan", 9.0f);
        Font Border = new Font("League Spartan SemiBold", 9.0f);
        string uid;
        private async void Danhsach_album()
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
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client1 = new FireSharp.FirebaseClient(Config);
            uid = client.User.Uid;
            CRUD_album album = new CRUD_album();
            Task<Dictionary<string, Dictionary<string, object>>> album1 = album.Lay_album(uid);

            var result = await album1;
            // Kiểm tra xem tác vụ có thành công không
            if (album1.Status == TaskStatus.RanToCompletion)
            {
                // Truy xuất danh sách
                albumtruyen = result;
            }
            else
            {
                TextBox textBox = new TextBox();
                Font font = new Font("League Spartan SemiBold", 24.0f);
                textBox.Text = "Lỗi! Không thể tải Album truyện.";
                textBox.Font = font;
                textBox.ForeColor = Color.Red;
            }    

        }
        private void Khoi_tao_album()
        {

            // Truy xuất từ điển trong mỗi phần tử
            foreach (var dictionary in albumtruyen)
            {
                idtruyen.Add(dictionary.Key);
                // Truy xuất giá trị từ điển thông qua khóa
                Dictionary<string, object> innerDictionary = dictionary.Value;
                //xem xem có bao nhiêu truyện được lưu trong album
                noidung_album.Add(innerDictionary);
            }
        }
        private void Album(int start, int end)
        {
            int sl_truyen = noidung_album.Count;
            if (sl_truyen != 0)
            {
                int an_panel = end - sl_truyen;
                if (an_panel != 0)
                    panelTruyen10.Visible = false;
                if (an_panel - 1 != 0)
                    panelTruyen9.Visible = false;
                if (an_panel - 2 != 0)
                    panelTruyen8.Visible = false;
                if (an_panel - 3 != 0)
                    panelTruyen7.Visible = false;
                if (an_panel - 4 != 0)
                    panelTruyen6.Visible = false;
                if (an_panel - 5 != 0)
                    panelTruyen5.Visible = false;
                if (an_panel - 6 != 0)
                    panelTruyen4.Visible = false;
                if (an_panel - 7 != 0)
                    panelTruyen3.Visible = false;
                if (an_panel - 8 != 0)
                    panelTruyen2.Visible = false;

                for (int i = start; i < sl_truyen; i++)
                {
                    Dictionary<string, object> temp = new Dictionary<string, object>(noidung_album[i]);
                    // Chuyển chuỗi Base64 thành mảng byte
                    byte[] imageBytes = Convert.FromBase64String(temp["image"].ToString());
                    // Tạo MemoryStream từ mảng byte
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        // Đọc hình ảnh từ MemoryStream
                        Image image = Image.FromStream(ms);

                        // Hiển thị hình ảnh trên PictureBox
                        if (i == start)
                            ptrAnh1.Image = image;
                        else if (i == start + 1)
                            ptrAnh2.Image = image;
                        else if (i == start + 2)
                            ptrAnh3.Image = image;
                        else if (i == start + 3)
                            ptrAnh4.Image = image;
                        else if (i == start + 4)
                            ptrAnh5.Image = image;
                        else if (i == start + 5)
                            ptrAnh6.Image = image;
                        else if (i == start + 6)
                            ptrAnh7.Image = image;
                        else if (i == start + 7)
                            ptrAnh8.Image = image;
                        else if (i == start + 8)
                            ptrAnh9.Image = image;
                        else if (i == start + 9)
                            ptrAnh10.Image = image;
                    }
                    if (i == start)
                    {
                        lbTenTruyen1.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong1.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc1.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia1.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang1.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang1.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang1.Text = "Hoàn thành";
                        }
                        btnTheloai1.Text = temp["The_loai"].ToString();
                        panelTruyen1.Tag = idtruyen[i];
                    }  
                    else if (i == start + 1)
                    {
                        lbTenTruyen2.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong2.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc2.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia2.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang2.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang2.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang2.Text = "Hoàn thành";
                        }
                        btnTheloai2.Text = temp["The_loai"].ToString();
                        panelTruyen2.Tag = idtruyen[i];
                    }
                    else if (i == start + 2)
                    {
                        lbTenTruyen3.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong3.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc3.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia3.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang3.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang3.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang3.Text = "Hoàn thành";
                        }
                        btnTheloai3.Text = temp["The_loai"].ToString();
                        panelTruyen3.Tag = idtruyen[i];
                    }
                    else if (i == start + 3)
                    {
                        lbTenTruyen4.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong4.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc4.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia4.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang4.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang4.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang4.Text = "Hoàn thành";
                        }
                        btnTheloai4.Text = temp["The_loai"].ToString();
                        panelTruyen4.Tag = idtruyen[i];
                    }
                    else if (i == start + 4)
                    {
                        lbTenTruyen5.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong5.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc5.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia5.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang5.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang5.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang5.Text = "Hoàn thành";
                        }
                        btnTheloai5.Text = temp["The_loai"].ToString();
                        panelTruyen5.Tag = idtruyen[i];
                    }
                    else if (i == start + 5)
                    {
                        lbTenTruyen6.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong6.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc6.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia6.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang6.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang6.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang6.Text = "Hoàn thành";
                        }
                        btnTheloai6.Text = temp["The_loai"].ToString();
                        panelTruyen6.Tag = idtruyen[i];
                    }
                    else if (i == start + 6)
                    {
                        lbTenTruyen7.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong7.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc7.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia7.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang7.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang7.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang7.Text = "Hoàn thành";
                        }
                        btnTheloai7.Text = temp["The_loai"].ToString();
                        panelTruyen7.Tag = idtruyen[i];
                    }
                    else if (i == start + 7)
                    {
                        lbTenTruyen8.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong8.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc8.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia8.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang8.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang8.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang8.Text = "Hoàn thành";
                        }
                        btnTheloai8.Text = temp["The_loai"].ToString();
                        panelTruyen8.Tag = idtruyen[i];
                    }
                    else if (i == start + 8)
                    {
                        lbTenTruyen9.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong9.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc9.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia9.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang9.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang9.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang9.Text = "Hoàn thành";
                        }
                        btnTheloai9.Text = temp["The_loai"].ToString();
                        panelTruyen9.Tag = idtruyen[i];
                    }
                    else if (i == start + 9)
                    {
                        lbTenTruyen10.Text = temp["Tentruyen"].ToString();
                        ibtnsochuong10.Text = "  " + temp["tong_chuong"].ToString();
                        ibtnchuongdangdoc10.Text = "  " + temp["Chuong_dangdoc"].ToString();
                        btnTacgia10.Text = temp["Tacgia"].ToString();
                        if (Convert.ToInt32(temp["Trang_thai"]) == 0)
                        {
                            btnTinhtrang10.Text = "Dừng cập nhật";
                        }
                        else if (Convert.ToInt32(temp["Trang_thai"]) == 1)
                        {
                            btnTinhtrang10.Text = "Đang cập nhật";
                        }
                        else
                        {
                            btnTinhtrang10.Text = "Hoàn thành";
                        }
                        btnTheloai10.Text = temp["The_loai"].ToString();
                        panelTruyen10.Tag = idtruyen[i];
                    }
                }
            }
            else
            {
                panelAlbumTruyen.Visible = false;
                ibtnChiaseAlbum.Visible = false;
            }
            
        }

        private void deleteButton_Click(object sender, EventArgs e)
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
            var uid = client.User.Uid;
            IconButton deleteButton = (IconButton)sender; // Ép kiểu sender thành Button
            System.Windows.Forms.Panel panel = deleteButton.Parent as System.Windows.Forms.Panel;
            if (panel != null)
            {
                // Lấy thông tin truyện từ panel
                string idtruyen = panel.Tag as string;

                if (!string.IsNullOrEmpty(idtruyen))
                {
                    // Thực hiện xóa truyện dựa trên truyenId
                    Xoa_truyen_theo_idtruyen(idtruyen, uid);

                    // Xóa panel khỏi giao diện người dùng
                    panel.Parent.Controls.Remove(panel);
                }
            }
        }
        private async void Xoa_truyen_theo_idtruyen(string idtruyen, string uid)
        {
            CRUD_album album = new CRUD_album();
            await album.xoa_album(uid, idtruyen);
        }
        private async void Notification_turnon_Click(object sender, EventArgs e)
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
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client1 = new FireSharp.FirebaseClient(Config);
            var uid = client.User.Uid;
            IconButton iconButton = (IconButton)sender;
            Panel panel = iconButton.Parent as Panel;
            // Lấy thông tin truyện từ panel
            string idtruyen = panel.Tag as string;
            string buttonName = iconButton.Name;
            if (albumtruyen.ContainsKey(idtruyen))
            {
                Dictionary<string, object> thong_tin_truyen = albumtruyen[idtruyen];
                Them_Lay_thongbao thongbao = new Them_Lay_thongbao();
                await thongbao.Tat_Bat_thongbao_album(uid, idtruyen, thong_tin_truyen["Chuong_dangdoc"].ToString(),
                    thong_tin_truyen["Tentruyen"].ToString(), thong_tin_truyen["Tacgia"].ToString(), thong_tin_truyen["image"].ToString(),
                    thong_tin_truyen["The_loai"].ToString(), Convert.ToInt32(thong_tin_truyen["Trang_thai"].ToString()),
                    Convert.ToInt32(thong_tin_truyen["tong_chuong"].ToString()), false);
            }
            if (buttonName == "ibtnTatthongbao1")
            {
                ibtnThongbao1.BringToFront();
            }    
            else if (buttonName == "ibtnTatthongbao2")
            {
                ibtnThongbao2.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao3")
            {
                ibtnThongbao3.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao4")
            {
                ibtnThongbao4.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao5")
            {
                ibtnThongbao5.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao6")
            {
                ibtnThongbao6.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao7")
            {
                ibtnThongbao7.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao8")
            {
                ibtnThongbao8.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao9")
            {
                ibtnThongbao9.BringToFront();
            }
            else if (buttonName == "ibtnTatthongbao10")
            {
                ibtnThongbao10.BringToFront();
            }

        }
        private async void Notification_turnoff_Click(object sender, EventArgs e)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client1 = new FireSharp.FirebaseClient(Config);
            IconButton iconButton = (IconButton)sender;
            Panel panel = iconButton.Parent as Panel;
            // Lấy thông tin truyện từ panel
            string idtruyen = panel.Tag as string;
            string buttonName = iconButton.Name;
            if (albumtruyen.ContainsKey(idtruyen))
            {
                Dictionary<string, object> thong_tin_truyen = albumtruyen[idtruyen];
                Them_Lay_thongbao thongbao = new Them_Lay_thongbao();
                await thongbao.Tat_Bat_thongbao_album(uid, idtruyen, thong_tin_truyen["Chuong_dangdoc"].ToString(),
                    thong_tin_truyen["Tentruyen"].ToString(), thong_tin_truyen["Tacgia"].ToString(), thong_tin_truyen["image"].ToString(),
                    thong_tin_truyen["The_loai"].ToString(), Convert.ToInt32(thong_tin_truyen["Trang_thai"].ToString()),
                    Convert.ToInt32(thong_tin_truyen["tong_chuong"].ToString()), false);
            }
            if (buttonName == "ibtnThongbao1")
            {
                ibtnTatthongbao1.BringToFront();
            }
            else if (buttonName == "ibtnThongbao2")
            {
                ibtnTatthongbao2.BringToFront();
            }
            else if (buttonName == "ibtnThongbao3")
            {
                ibtnTatthongbao3.BringToFront();
            }
            else if (buttonName == "ibtnThongbao4")
            {
                ibtnTatthongbao4.BringToFront();
            }
            else if (buttonName == "ibtnThongbao5")
            {
                ibtnTatthongbao5.BringToFront();
            }
            else if (buttonName == "ibtnThongbao6")
            {
                ibtnTatthongbao6.BringToFront();
            }
            else if (buttonName == "ibtnThongbao7")
            {
                ibtnTatthongbao7.BringToFront();
            }
            else if (buttonName == "ibtnThongbao8")
            {
                ibtnTatthongbao8.BringToFront();
            }
            else if (buttonName == "ibtnThongbao9")
            {
                ibtnTatthongbao9.BringToFront();
            }
            else if (buttonName == "ibtnThongbao10")
            {
                ibtnTatthongbao10.BringToFront();
            }
        }

        private void lbTenanh1_Click(object sender, EventArgs e)
        {

        }
    }
}
