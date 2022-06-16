﻿using System;
using System.Net;
using System.Net.Sockets;

namespace ChatRoomServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 4099;
            Console.WriteLine("==============");
            var listener = new TcpListener(IPAddress.Any,port);
            try
            {
                Console.WriteLine("Server start at port {0}", port);
                listener.Start();

                Console.WriteLine("Waiting for a connection... ");
                var client = listener.AcceptTcpClient();

                var address = client.Client.RemoteEndPoint.ToString();
                Console.WriteLine("Client has connected from {0}", address);

                while (true)
                {
                    Receive(client);
                    System.Threading.Thread.Sleep(1000);
                }

                client.Close();
                Console.WriteLine("Disconnect client {0}", address);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                listener.Stop();
                Console.WriteLine("Server shutdown");
                Console.Read();
            }
        }
        private static void Receive(TcpClient client)
        {
            var stream = client.GetStream();

            var numBytes = client.Available;
            if (numBytes == 0)
            {
                return;
            }

            var buffer = new byte[numBytes];
            var bytesRead = stream.Read(buffer, 0, numBytes);

            var request = System.Text.Encoding.Unicode.GetString(buffer).Substring(0, bytesRead);
            Console.WriteLine("Text: " + request);
        }
    }
}
