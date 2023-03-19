using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;

namespace Server
{
    public class Room
    {

        public Room(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public Player playerHost;
        public Player playerJoin;

        public int nbPlayer = 0;
        public int Port { get; set; }



        public static void ReceiveMessages(UdpClient clientSocket,Player player)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] receivedData = clientSocket.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.ASCII.GetString(receivedData);
                Console.WriteLine("Received from " + remoteEndPoint.ToString() + ": " + receivedMessage);
                if (receivedMessage.ToUpper().Equals("READY"))
                {
                    player.ready = true;
                }
                while (true) //score 
                {
                     //cordinate paddel
                     receivedData = clientSocket.Receive(ref remoteEndPoint);
                     receivedMessage = Encoding.ASCII.GetString(receivedData);
                     
                }

            }
        }

    }
}
