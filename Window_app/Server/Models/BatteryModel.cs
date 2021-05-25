using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Models
{
    class BatteryModel
    {
        public double current { set; get; }
        public double voltage { set; get; }
        public double energy { set; get; }
    }
}
