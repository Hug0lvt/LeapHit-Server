using System;
using System.Net;
using System.Net.Sockets;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to LeapHit Multiplayer - Server");
        StartServer();
    }

    static void StartServer()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.41.58"), 3131);
        UdpClient server = new UdpClient(endPoint);
        Console.WriteLine("Server started, waiting for clients to connect...");

        while (true)
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = server.Receive(ref clientEndPoint);
            string dataReceived = System.Text.Encoding.ASCII.GetString(data);
            Console.WriteLine("Data received from client: " + dataReceived + " from " + clientEndPoint.ToString());
        }
    }
}
