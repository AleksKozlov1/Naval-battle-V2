using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class ServerHandler
    {
        private Socket listener;
        private CancellationTokenSource cts;
        private bool isStart;
        Socket[] sockets = { null, null };

        public ServerHandler(string ip, int port)
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            cts = new CancellationTokenSource();
            isStart = false;
            listener.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
        }
        public void Start()
        {
            if (!isStart)
            {
                listener.Listen(2);
                isStart = true;
                Console.WriteLine("Сервер запущен");
                AcceptClients();
            }
        }
        public void Stop()
        {
            if (isStart)
            {
                isStart = false;
                cts.Cancel();
                listener.Close();
            }
        }
        private void AcceptClients()
        {
            while (isStart || !cts.IsCancellationRequested)
            {
                try
                {
                    Socket clientSocket = listener.Accept();
                    if (sockets[0] == null)
                    {
                        sockets[0] = clientSocket;
                        Task.Run(() => HandleClient(sockets[0], 1), cts.Token);
                    }
                    else
                    {
                        sockets[1] = clientSocket;
                        Task.Run(() => HandleClient(sockets[1], 0), cts.Token);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения клиента: {ex.Message}");
                }
            }
        }
        private void HandleClient(Socket clientSocket, int numSecond)
        {
            Console.WriteLine($"Клиент подключен: {clientSocket.RemoteEndPoint}");

            int[] status = { 0, 0 };
            if (numSecond == 1)
                status[0] = 1;
            else
                status[0] = 2;

            // Цикл отправки статуса игры (номер очереди игрока, статус второго)
            while (clientSocket.Connected)
            {
                Thread.Sleep(1000);

                if (sockets[numSecond] != null)
                {
                    status[1] = 1;
                    SendArray(status, clientSocket);
                    Console.WriteLine(status[0].ToString() + " " + status[1].ToString());
                    break;
                }
                // Клиент дальше ждет второго игрока
                status[1] = 0;
                SendArray(status, clientSocket);
                Console.WriteLine(status[0].ToString() + " " + status[1].ToString());
            }

            while (clientSocket.Connected)
            {
                if (sockets[numSecond] != null)
                {
                    int[] array = ReceiveArray(sockets[numSecond]);
                    SendArray(array, clientSocket);
                }
            }
            clientSocket.Close();
        }
        public void SendArray(int[] message, Socket client)
        {
            byte[] byteArray = new byte[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                byteArray[i] = (byte)message[i];
            }

            client.SendAsync(byteArray);
        }
        public int[] ReceiveArray(Socket client)
        {
            byte[] buffer = new byte[1024];
            int size = client.Receive(buffer);
            int[] message = new int[size];
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = (int)buffer[i];
            }
            return message;
        }
    }
}
