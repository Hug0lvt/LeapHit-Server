using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOPlayer
    {
        public int playerId { get; set; }
        public string name { get; set; }
        public int nbBallTouchTotal { get; set; }
        public int timePlayed { get; set; }
    }
}
