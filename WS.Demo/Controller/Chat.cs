using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WS.Demo.Controller
{
    public class Chat : WebSocketBehavior
    {
        private string _name;
        private static int _number = 0;
        private string _prefix;

        public Chat() : this(null)
        {
        }

        public Chat(string prefix)
        {
            _prefix = !prefix.IsNullOrEmpty() ? prefix : "匿名#";
        }

        private string GetName()
        {
            var name = Context.QueryString["name"];
            return !name.IsNullOrEmpty() ? name : _prefix + GetNumber();
        }

        private static int GetNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            string message = string.Format("{0} 離開對話...", _name);
            Console.WriteLine(message);
            Sessions.Broadcast(message);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            string message = string.Format("{0}: {1}", _name, e.Data);
            Console.WriteLine(message);
            Sessions.Broadcast(message);
        }

        protected override void OnOpen()
        {
            _name = GetName();
            string message = string.Format("{0} 加入對話...", _name);
            Console.WriteLine(message);
            Sessions.Broadcast(message);
        }
    }
}