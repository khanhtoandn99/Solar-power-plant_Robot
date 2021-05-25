using Newtonsoft.Json;
using SolarServer.Data;
using SolarServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Commands
{
    class SetModeCommand: BaseCommand
    {
        public override void Exec()
        {
            var listMachine = JsonConvert.DeserializeObject<List<SolarMachine>>(SecondParameter);
            foreach (var machine in listMachine)
            {
                var realMachine = MachineManager.Instance().MachineList.FirstOrDefault(m => m.IDRobot == machine.IDRobot);
                if (realMachine!=null)
                {
                    var updateModeCommand = new
                    {
                        Type = 9,
                        Data = machine.Mode
                    };

                    realMachine.SendRawCmd(JsonConvert.SerializeObject(updateModeCommand));
                }
            }
             
        }
    }
}
