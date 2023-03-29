using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class GameEntities
    {
        public Tuple<float, float> Ball { get; set; }
        public float Paddle { get; set; }

        public GameEntities(Tuple<float, float> ball, float paddle)
        {
            Ball = ball;
            Paddle = paddle;
        }
    }
}
