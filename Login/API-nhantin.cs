using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace Nhantin
{
    public class message
    {
        public string Noi_dung { get; set; }
        public string TG_gui { get; set; }

        public message(string noidung, string tg)
        {
            Noi_dung = noidung;
            TG_gui = tg;
        }

        public message() { }
    }

    public class MessageAPI
    {
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        public async Task SendMessage(string senderID, string receiverID, string noidung)
        {
            var Time = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt");

            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);

            var senderPath = $"Nguoi_dung/{senderID}/Nhan_tin/{receiverID}/Tin_nhan_gui";
            var senderMessage = new message(noidung, Time);
            PushResponse senderResponse = await client.PushAsync(senderPath, senderMessage);

            var receiverPath = $"Nguoi_dung/{receiverID}/Nhan_tin/{senderID}/Tin_nhan_nhan";
            var receiverMessage = new message(noidung, Time);
            PushResponse receiverResponse = await client.PushAsync(receiverPath, receiverMessage);
        }

        public async Task<List<message>> ReceiveMessage(string userID, string senderID)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);

            var path = $"Nguoi_dung/{userID}/Nhan_tin/{senderID}/Tin_nhan_nhan";

            FirebaseResponse res = await client.GetAsync(path);

            List<message> result = new List<message>();

            /*if (res != null && res.Body != null)
            {
                // Chuyển đổi dữ liệu JSON thành danh sách tin nhắn
                var messageData = JsonConvert.DeserializeObject(res.Body);
            // Nếu danh sách tin nhắn không null, thêm vào danh sách kết quả
            if (messageData != null)
                {
                    result.AddRange(messageData);
                }
            }*/
            return result;
        }
    }
}
