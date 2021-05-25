using Newtonsoft.Json;
using SolarServer.Commands;
using SolarServer.Data;
using SolarServer.Events;
using SolarServer.Models;
using SolarServer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolarServer.Connection
{
    class UniversalSocketHandler
    {
        TcpClient clientSocket;
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(run);
            ctThread.Start();
        }
        private void run()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            requestCount = 0;
            NetworkStream networkStream = clientSocket.GetStream();
            var reader = new StreamReader(networkStream);
            var writer = new StreamWriter(networkStream);
            writer.AutoFlush = true;

                try
                {
                    requestCount = requestCount + 1;
                    string str = reader.ReadLine();
                    var cmd = JsonConvert.DeserializeObject<BaseCommand>(str);
                    var realCmd = BaseCommand.GetCommand(cmd);
                    if (realCmd.Code!=CommandCode.REGISTER_CLIENT)
                    {
                        ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Arbort connection: "+ ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString()+":"+ ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Port,"Connection Handler"));
                    }
                    else 
                    {
                    if (realCmd.SocketClientType == ClientType.CLIENT_TYPE|| realCmd.SocketClientType==5)
                    {
                        ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Client connected: " + ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Port, "Connection Handler"));

                        ClientModel model = new ClientModel();
                        ClientSocketHandler client = new ClientSocketHandler();
                        client.startClient(reader, writer, clientSocket);
                        realCmd.IsSuccess = true;
                        model.ConnectionHandler = client;
                        model.type = realCmd.SocketClientType;
                        model.ID = realCmd.SendID;
                        model.Do(realCmd);
                        ClientManager.Instance().AddMachine(model);
                    }
                    else if (realCmd.SocketClientType == ClientType.MACHINE_TYPE)
                    {
                       
                        MachineSocketHandler client = new MachineSocketHandler();
                        client.startClient(reader, writer, clientSocket);
                        realCmd.IsSuccess = true;

                        var existingMachine = MachineManager.Instance().MachineList.FirstOrDefault(item => item.IDRobot == realCmd.SendID);
                        if (existingMachine!=null)
                        {
                            existingMachine.CommandConnectionHandler.Stop();
                            MachineManager.Instance().RemoveMachine(existingMachine.CommandConnectionHandler);
                        }
                        SolarMachine model = new SolarMachine();
                        var existingMachineConfig = SettingManager.Instance().MachineIDConfig.FirstOrDefault(item => item.MachineId == realCmd.SendID);
                        if (existingMachineConfig!=null)
                        {
                            model.ID = existingMachineConfig.ID;
                            model.Description = existingMachineConfig.Description;
                        } else
                        {
                            var machineConfig = new MachineIdModel
                            {
                                ID = SettingManager.Instance().MachineIDConfig.Count == 0 ? "0" : (int.Parse(SettingManager.Instance().MachineIDConfig.Max(item => item.ID)) + 1).ToString(),
                                MachineId = realCmd.SendID,
                                Description = ""
                            };
                            SettingManager.Instance().MachineIDConfig.Add(machineConfig);
                            SettingManager.Instance().SaveMachines();
                        }
                        model.CommandConnectionHandler = client;
                        model.IDRobot = realCmd.SendID;
                        model.ID = (MachineManager.Instance().MachineList.Count+1).ToString();
                        model.Do(realCmd);
                        var ack = new
                        {
                            Type = 20,
                            Data = 20
                        };
                        model.CommandConnectionHandler.SendRawCmd(JsonConvert.SerializeObject(ack));
                        var idCommand = new
                        {
                            Type = 2,
                            Data = model.ID
                        };
                        model.CommandConnectionHandler.SendRawCmd(JsonConvert.SerializeObject(idCommand));

                        ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Machine ID="+ model.IDRobot+" connected: " + ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Port, "Connection Handler"));
                        MachineManager.Instance().AddMachine(model);
                    } 
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
             
                }
            
        }
    }
}
