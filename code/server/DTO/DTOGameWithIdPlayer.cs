using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOGameWithIdPlayer
    {
        public int gameId { get; set; }
        public int durationGame { get; set; }
        public int nbMaxEchanges { get; set; }
        public string playerWinner { get; set; }
        public string playerLoser { get; set; }
    }
}
