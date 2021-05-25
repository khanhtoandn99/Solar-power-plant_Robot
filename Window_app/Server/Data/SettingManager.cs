using Newtonsoft.Json;
using Prism.Events;
using SolarServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Data
{
    class SettingManager
    {
        public const string SETTING_PATH = "config.json";
        public const string PLANTMAP_PATH = "plant_map.json";
        private static SettingManager _instance;
        public EventAggregator eventAggregator;
        public List<MachineIdModel> MachineIDConfig;
        private SettingManager()
        {
            MachineIDConfig = new List<MachineIdModel>();
        }
        public static SettingManager Instance()
        {
            if (_instance == null)
            {
                _instance = new SettingManager();
            }
            return _instance;
        }
      
        public void SaveMachines()
        {
            File.WriteAllText(SETTING_PATH, JsonConvert.SerializeObject(MachineIDConfig));
        }
        public void SavePlantMap()
        {
            File.WriteAllText(PLANTMAP_PATH, JsonConvert.SerializeObject(RowOfStringManager.Instance.RowOfPanelList));
        }
        public void GetMachineIDConfig()
        {
          
            try
            {
                MachineIDConfig = JsonConvert.DeserializeObject<List<MachineIdModel>>(File.ReadAllText(SETTING_PATH)); 
            } catch
            {
                MachineIDConfig = new List<MachineIdModel>();
            }
             
        }
        public void GetPlantMapConfig()
        {
            try
            {
                RowOfStringManager.Instance.InitFistList(JsonConvert.DeserializeObject<List<RowOfPanel>>(File.ReadAllText(PLANTMAP_PATH)));
            }
            catch
            {
                RowOfStringManager.Instance.InitFistList(new List<RowOfPanel>());
            }
        }

    }
}
