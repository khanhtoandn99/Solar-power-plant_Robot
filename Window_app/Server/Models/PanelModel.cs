using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Models
{
    class PanelModel
    {
        public Position location { set; get; }
        public double current { set; get; }
        public double voltage { set; get; }
    }
}
