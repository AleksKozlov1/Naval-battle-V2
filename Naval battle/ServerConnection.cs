using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Naval_battle
{
    internal class ServerConnection
    {
        private Socket server;
        private CancellationTokenSource cts;
        private IPEndPoint endPoint;
        private bool isConnected;
        public int[] array2D;
        public int arraySize = 10; // размер массива
        public ServerConnection(string ip = "127.0.0.1", int port = 8080)
        {
            array2D = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            cts = new CancellationTokenSource();
            isConnected = false;
            endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                server.Connect(endPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void Stop()
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
        // Просто отправка массива, на вход одномерный массив значений int,
        //      все можно поменять под конкретный, с получением также
        public void SendArray(int[] message)
        {
            byte[] byteArray = new byte[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                byteArray[i] = (byte)message[i];
            }

            server.SendAsync(byteArray);
        }

        public int[] ReceiveArray()
        {
            byte[] buffer = new byte[1024];
            int size = server.Receive(buffer);
            int[] message = new int[size];
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = (int)buffer[i];
            }
            return message;
        }
    }
}
