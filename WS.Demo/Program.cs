using System;
using WS.Demo.Controller;
using WebSocketSharp.Server;


namespace WS.Demo
{
    static class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer(55688);

            wssv.AddWebSocketService<Chat>("/Chat");
            wssv.AddWebSocketService<Echo>("/Echo");
            
            wssv.Start();
            if (wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                {
                    Console.WriteLine("- {0}", path);
                }
            }

            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();
            wssv.Stop();
        }
    }
}