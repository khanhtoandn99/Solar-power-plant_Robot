using SolarServer.Commands;
using SolarServer.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Models
{
    public class ClientModel
    {
        public string ID { set; get; }
        public int type { set; get; }
        public ClientSocketHandler ConnectionHandler { set; get; }
        public void Do(BaseCommand cmd)
        {

            ConnectionHandler.Send(cmd);
        }
    }
}
