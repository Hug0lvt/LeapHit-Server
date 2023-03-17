using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Room
    {

        public Room(string id)
        {
            ID = id;
        }

        public string ID { get; set; }

        public Player playerHost;
        public Player playerJoin;

        public int nbPlayer = 0;
        public int Port { get; set; }
    }
}
