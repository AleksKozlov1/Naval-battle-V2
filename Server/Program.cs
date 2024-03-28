namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerHandler server = new ServerHandler("127.0.0.1", 8080);
            server.Start();
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.Stop();
        }
    }
}
