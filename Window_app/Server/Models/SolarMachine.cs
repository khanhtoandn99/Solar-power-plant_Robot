using Newtonsoft.Json;
using SolarServer.Commands;
using SolarServer.Connection;
using SolarServer.Data;
using SolarServer.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Models
{
    public class SolarMachine
    {
        [JsonIgnore]
        public String Name {
            get
            {
                return "Machine: ID=" + ID + "/" + IDRobot;
            }
        }
        public SolarMachine()
        {
            Logs = new ObservableCollection<MachineLog>();
        }
        [JsonIgnore]
        public ObservableCollection<MachineLog> Logs {
            set; get;
        }
        public string ID { set; get; }
        [JsonIgnore]
        public MachineSocketHandler CommandConnectionHandler { set; get; }
        [JsonIgnore]
        public ImageStreamSocketHandler ImageStreamConnectionHandler { set; get; }
        [JsonIgnore]
        public List<ClientModel> ClientOb { set; get; }
        public String IDRobot { set; get; }
        public Position Position { set; get; }
        public String AMP { set; get; }
        public String Volt { set; get; }
        public String BatteryPercent { set; get; }
        public string Status { set; get; }
        public string CurrentErrorCode { set; get; }
        public bool IsOnline { set; get; }
        public string Mode { set; get; } = "1";
        public string Direction { set; get; }
        public string Description { set; get; }
        public int SpeedMoving { set; get; }

        public int SpinnerSpeed { set; get; }

        public int ChargingThreshold { set; get; }

        public int MinPower { set; get; }

        public int MaxPower { set; get; }
        public void Do(BaseCommand cmd)
        {

        }
        public void SendRawCmd (string jsonCommand)
        {
            
            if (CommandConnectionHandler!=null)
            {
                
                CommandConnectionHandler.SendRawCmd(jsonCommand);
            }
            
        }
    }
}
