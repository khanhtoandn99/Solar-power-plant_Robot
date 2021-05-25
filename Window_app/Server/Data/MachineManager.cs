using Newtonsoft.Json;
using Prism.Events;
using SolarServer.Commands;
using SolarServer.Connection;
using SolarServer.Events;
using SolarServer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SolarServer.Data
{
    class MachineManager
    {
        public Dispatcher dispatcher;
        private ObservableCollection<SolarMachine> _machines;
        private BlockingCollection<SolarMachine> _workingMachines;
        public ObservableCollection<SolarMachine> MachineList
        {
            get
            {
                return _machines;
            }
            set
            {
                _machines = value;
            }
        }
        public BlockingCollection<SolarMachine> WorkingMachines
        {
            get
            {
                return _workingMachines;
            }
            set
            {
                _workingMachines = value;
            }
        }
        private static MachineManager _instance;
        private MachineManager()
        {
            var solarMachines = new ObservableCollection<SolarMachine>();
            /*
            solarMachines.Add(new SolarMachine
            {
                IDRobot = "114021062019",
                ID = "1",
                AMP = "2",
                Volt = "24",
                BatteryPercent = "99",
                MinPower = 10,
                MaxPower = 80,
                SpeedMoving = 12,
                SpinnerSpeed = 10,
                ChargingThreshold = 40
                

            });*/
            _machines = solarMachines;
        }
        public static MachineManager Instance()
        {
            if (_instance == null)
            {
                _instance = new MachineManager();
            }
            return _instance;
        }
        public void AddLog(SolarMachine machine, MachineLog log)
        {
            if (dispatcher != null)
            {
                dispatcher.BeginInvoke((Action)(() => {
                  machine.Logs.Insert(0,log);
                }
                ));
            }
        }
        public bool IsExist(string ID)
        {
            if (_machines.Any(item => item.ID.Equals(ID)))
            {
                return true;
            } else
            {
                return false;
            }
        }
        public int Count()
        {
            return _machines.Count;
        }
        public void AddMachine(SolarMachine solarMachine)
        {
            if (_machines.Any(item => item.ID.Equals(solarMachine.ID)))
            {
                return;
            }
            else
            {
                if (dispatcher!=null)
                {
                    dispatcher.BeginInvoke((Action)(() => {
                        _machines.Add(solarMachine);
                    }
                    ));
                }
                ApplicationService.Instance().CurrentEventAggregator.GetEvent<NewClientConnectedEvent>().Publish();

            }
           
        }
        public SolarMachine GetMachine(string ID)
        {
            return _machines.FirstOrDefault(machine => machine.IDRobot.Equals(ID));
        }
        public void RemoveMachine(string ID)
        {
            _machines.Remove(_machines.Where(i => i.ID.Equals(ID)).FirstOrDefault());
        }
        public void RemoveMachine(MachineSocketHandler tcpClient)
        {
            if (dispatcher != null)
            {
                dispatcher.BeginInvoke((Action)(() => {
                    _machines.Remove(_machines.Where(i => i.CommandConnectionHandler == tcpClient).FirstOrDefault());
                }
                ));
            }
          
            tcpClient.Stop();

        }
        public void RemoveMachine(SolarMachine machine)
        {
      
            _machines.Remove(machine);
        }
        public void BroadCast(BaseCommand cmd)
        {
            for (int i = 0; i <= _machines.Count; i++)
            {
                try
                {
                    _machines[i].Do(cmd);
                } catch
                {

                }
            }
        }
        public void Stop()
        {
            for (int i= 0; i<=_machines.Count;i++)
            {
                try
                {
                    _machines[i].CommandConnectionHandler.Stop();
                }catch
                {

                }
               
            }
            _machines.Clear();
        }
    }
}
