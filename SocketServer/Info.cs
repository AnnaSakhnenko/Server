using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Info
    {
        public string email;
        public string where;
        public string text;
    }

    public class InfoList
    {
        public InfoList() { }
        public List<Info> list;
    }
}
