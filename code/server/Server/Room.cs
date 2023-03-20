using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using System.ComponentModel;

namespace Server
{
    public class Room : INotifyPropertyChanged
    {

        public Room(string id, bool availaible)
        {
            Id = id;
            Availaible = availaible;
        }

        public bool Availaible { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Id { get; set; }

        public Player playerHost;
        public Player playerJoin;
        public int Port { get; set; }


        public int nbPlayer = 0;

        public bool maxPlayer
        {
            get { return nbPlayer >= 2; }
            set
            {
                if (nbPlayer >= 2)
                {
                    maxPlayer = true;
                    NotifyPropertyChanged("Ready");
                }
            }
        }

        
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void ReceiveMessages(UdpClient clientSocket,Player player)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] receivedData = clientSocket.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.ASCII.GetString(receivedData);
                Console.WriteLine("Received from " + remoteEndPoint.ToString() + ": " + receivedMessage);
                
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
