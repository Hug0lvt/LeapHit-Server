﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOGame
    {
        public int gameId { get; set; }
        public int durationGame { get; set; }
        public int nbMaxEchanges { get; set; }
        public int playerWinner { get; set; }
        public int playerLoser { get; set; }
    }
}
