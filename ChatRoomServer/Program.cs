using System;


namespace ChatRoomServer
{
    class Program
    {
        static HashSet<TcpClient> clients = new HashSet<TcpClient>();
        static void Main(string[] args)
        {
            Console.WriteLine("====================================");
            var server = new ChatCore.ChatServer();
            server.Bind(4099);
            server.Start();
        }
    }
}
