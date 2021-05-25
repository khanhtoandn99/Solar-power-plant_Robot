using Newtonsoft.Json;
using SolarServer.Data;
using SolarServer.Events;
using SolarServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Commands
{
    class InitializeCommand : BaseCommand
    {
        public override void Exec()
        {
            var solarMachineListData = JsonConvert.DeserializeObject<List<SolarMachine>>(SecondParameter);
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Client " + this.SendID + " initialize machines", "Client"));
            if (solarMachineListData != null)
            {
                foreach (var item in solarMachineListData)
                {
                    var existingConfig = SettingManager.Instance().MachineIDConfig.FirstOrDefault(machineConfig => machineConfig.MachineId == item.IDRobot);
                    if (existingConfig != null)
                    {
                        existingConfig.Description = item.Description;
                        existingConfig.ID = item.ID;
                    }
                    var existingMachine = MachineManager.Instance().MachineList.FirstOrDefault(machine => machine.IDRobot == item.IDRobot);
                    if (existingMachine != null)
                    {
                        existingMachine.Description = item.Description;
                        existingMachine.ID = item.ID;
                    }

                }
                SettingManager.Instance().SaveMachines();
            }
        }
    }
}
