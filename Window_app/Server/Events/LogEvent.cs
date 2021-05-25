using Prism.Events;
using SolarServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Events
{
    public class LogEvent : PubSubEvent<LogModel>
    {
    }
}
