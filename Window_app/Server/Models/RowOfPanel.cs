using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Models
{
	class RowOfPanel
	{
		public String Id { set; get; }

		public String nameString { set; get; }
        public List<SolarPanel> solarPanels { set; get; } = new List<SolarPanel>();

		public String description { set; get; }
	}
	
}
