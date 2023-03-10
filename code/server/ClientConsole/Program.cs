using System;
using System.Net;
using System.Net.Sockets;



class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Pong Multiplayer - Client");
        StartClient();
    }

    static void StartClient()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3131);

        //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("82.165.19.245"), 3131);
        //IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses("leaphit.hulivet.fr").FirstOrDefault(), 3131);
        UdpClient client = new UdpClient();
        Console.WriteLine("Client connected to server: " + endPoint.ToString());

        while (true)
        {
            Console.WriteLine("Enter data to send to server:");
            string data = Console.ReadLine();
            byte[] dataToSend = System.Text.Encoding.ASCII.GetBytes(data);
            client.Send(dataToSend, dataToSend.Length, endPoint);
        }
    }
}
