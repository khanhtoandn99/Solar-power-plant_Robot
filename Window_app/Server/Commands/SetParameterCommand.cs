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
    class SetParameterCommand : BaseCommand
    {
        public override void Exec()
        {
         
            var solarMachineData = JsonConvert.DeserializeObject<SolarMachine>(SecondParameter);
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Client " + this.SendID + " set parameter for machine: "+solarMachineData.IDRobot, "Client"));
     
            var solarMachine = MachineManager.Instance().GetMachine(solarMachineData.IDRobot);
            if (solarMachine != null)
            {
                solarMachine.IDRobot = solarMachineData.IDRobot;
                solarMachine.SpeedMoving = solarMachineData.SpeedMoving;
                solarMachine.SpinnerSpeed = solarMachineData.SpinnerSpeed;
                solarMachine.MaxPower = solarMachineData.MaxPower;
                solarMachine.MinPower = solarMachineData.MinPower;
                solarMachine.ChargingThreshold = solarMachineData.ChargingThreshold;
                var updateSpeedCmd = new RawCommand();
                updateSpeedCmd.Data = solarMachine.SpeedMoving.ToString();
                updateSpeedCmd.Type = 3;
                solarMachine.SendRawCmd(JsonConvert.SerializeObject(updateSpeedCmd));
                updateSpeedCmd.Data = solarMachine.SpinnerSpeed.ToString();
                updateSpeedCmd.Type = 5;
                solarMachine.SendRawCmd(JsonConvert.SerializeObject(updateSpeedCmd));
                updateSpeedCmd.Data = solarMachine.MaxPower.ToString();
                updateSpeedCmd.Type = 6;
                solarMachine.SendRawCmd(JsonConvert.SerializeObject(updateSpeedCmd));
                updateSpeedCmd.Data = solarMachine.MinPower.ToString();
                updateSpeedCmd.Type = 7;
                solarMachine.SendRawCmd(JsonConvert.SerializeObject(updateSpeedCmd));
                updateSpeedCmd.Data = solarMachine.ChargingThreshold.ToString();
                updateSpeedCmd.Type = 4;
                solarMachine.SendRawCmd(JsonConvert.SerializeObject(updateSpeedCmd));
            }
           
        }
    }
}
