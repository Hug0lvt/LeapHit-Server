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
        public ICollection<Player> MaxPlayers { get; set; }
        public int Port { get; set; }
    }
}
