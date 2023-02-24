using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOChat
    {
        public int chatId { get; set; }
        public DTOPlayer PlayerId1 { get; set; }
        public DTOPlayer PlayerId2 { get; set; }
    }
}
