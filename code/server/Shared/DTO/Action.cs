using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public enum Action
    {
        Join,
        Host,
        Connect,
        End,

        SendPlayer,
        RecivePlayer,
        SendEntities,
        SendScore
    }
}
