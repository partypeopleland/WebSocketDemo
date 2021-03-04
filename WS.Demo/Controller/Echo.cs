using System;
using System.Text;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WS.Demo.Controller
{
    public class Echo : WebSocketBehavior
    {
        private bool _sendTimeStamp = true;
        protected override void OnOpen()
        {
            Console.WriteLine("客戶端開啟~");
            _sendTimeStamp = true;
            while (_sendTimeStamp)
            {
                var message = "現在時間:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ssfff");
                Console.WriteLine(message);
                Sessions.BroadcastAsync(Encoding.UTF8.GetBytes(message), null);
                Thread.Sleep(500);
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _sendTimeStamp = false;
            Console.WriteLine("客戶端關閉~");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var name = Context.QueryString["name"];
            this.Send(!name.IsNullOrEmpty() ? string.Format("\"{0}\" to {1}", e.Data, name) : e.Data);
        }
    }
}