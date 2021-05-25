using SolarServer.Commands;
using SolarServer.Connection;
using SolarServer.Events;
using SolarServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SolarServer.Data
{
    class ClientManager
    {
        private List<ClientModel> _clients;
        private static ClientManager _instance;
        private ClientManager()
        {
            _clients = new List<ClientModel>();
        }
        public static ClientManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ClientManager();
            }
            return _instance;
        }
        public int Count()
        {
            return _clients.Count;
        }
        public bool IsExist(string ID)
        {
            if (_clients.Any(item => item.ID.Equals(ID)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddMachine(ClientModel solarMachine)
        {
            _clients.Add(solarMachine);
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<NewClientConnectedEvent>().Publish();
            SettingManager.Instance().SaveMachines();
        }
        public void GetMachine(string ID)
        {
            _clients.FirstOrDefault(machine => machine.ID.Equals(ID));
        }
        public void RemoveMachine(string ID)
        {
            _clients.Remove(_clients.Where(i => i.ID.Equals(ID)).FirstOrDefault());
        }
        public void RemoveMachine(ClientSocketHandler socketHandler)
        {
            _clients.Remove(_clients.Where(i => i.ConnectionHandler==socketHandler).FirstOrDefault());
            socketHandler.Stop();
        }
        public void RemoveMachine(ClientModel machine)
        {
            _clients.Remove(machine);
        }
        public void BroadCast(BaseCommand cmd,bool isIgnoreMobile = false)
        {
            for (int i=0;i<_clients.Count;i++)
            {
             //   Console.WriteLine("update to client");
                try
                {
                    if (isIgnoreMobile)
                    {
                        if (_clients[i].type != 5)
                        {
                            int z = i;
                          //  Thread t = new Thread(() =>
                           // {
                                try
                                {
                                    _clients[z].Do(cmd);

                                }
                                catch
                                {

                                }
                        //    }
                         //   );
                         //   t.Start();
                        }
                    } else
                    {
                       // Thread t = new Thread(() =>
                      //  {
                            int z = i;
                            try
                            {
                                _clients[z].Do(cmd);

                            } catch
                            {

                            }
                      //  }
                     //      );
                      //  t.Start();
                    }
                   
                    
                } catch
                {
                }
                
            }
        }
        public void Stop()
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                //   Console.WriteLine("update to client");
                try
                {
                    _clients[i].ConnectionHandler.Stop();
                }
                catch
                {

                }

            }
            _clients.Clear();
        }
    }
}

