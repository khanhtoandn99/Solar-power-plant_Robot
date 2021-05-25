using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Models
{
    class SolarPanel
    {
        SolarMachinePosition Position { set; get; }
        public string Current { set; get; }
        public string Volt { set; get; }
        public string Id { set; get; }
        public string ImageUrl { set; get; }
		public string IdString { set; get; }
		public String IdRobot { set; get; }
		public bool isHasRobot { set; get; }

		public string position { set; get; }

		public bool isCurrentPositionOfMachine { set; get; }

	}
    public class SolarMachinePosition
    {
        public int X { set; get; }
        public int Y { set; get; }
    }
}
