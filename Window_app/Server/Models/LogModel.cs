using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Models
{
    public class LogModel
    {
        public LogModel()
        {
            Time = DateTime.Now.ToString( "dd/MM hh:mm:ss");
        }
        public string Time { set; get; }
        public string Type { set; get; }
        public string Content { set; get; }
        public string Source { set; get; }
    }
    public class ErrorLogModel: LogModel
    {
        public ErrorLogModel(string content, string source)
        {
            Type = "Error";
            Content = content;
            Source = source;
        }
    }
    public class InfoLogModel : LogModel
    {
        public InfoLogModel(string content, string source)
        {
            Type = "Info";
            Content = content;
            Source = source;
        }
    }
    public class MachineLog
    {
        public string Time { set; get; }
        public string Type { set; get; }
        public string Content { set; get; }
        public MachineLog()
        {

        }
        public MachineLog(string content, string type)
        {
            Type = type;
            Content = content;
            Time = DateTime.Now.ToString("dd/MM hh:mm:ss");
        }
    }
}
