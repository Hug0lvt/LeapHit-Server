using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to LeapHit Multiplayer - Client");
        StartClient();
    }

    static void StartClient()
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3131);
        UdpClient client = new UdpClient();

        // Send connection message to server
        string connectionMessage = "Connect";
        byte[] connectionData = Encoding.ASCII.GetBytes(connectionMessage);
        client.Send(connectionData, connectionData.Length, serverEndPoint);

        // Receive connection message from server
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] receivedData = client.Receive(ref remoteEndPoint);
        string receivedPort = Encoding.ASCII.GetString(receivedData);
        Console.WriteLine("Received port: " + receivedPort);

        // Send data to server
        string message = "";
        while (message != "exit")
        {
            serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse(receivedPort));

            Console.Write("Enter message to send (or 'exit' to quit): ");
            message = Console.ReadLine();
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data, data.Length, serverEndPoint);
        }

        // Close client
        client.Close();
    }
}