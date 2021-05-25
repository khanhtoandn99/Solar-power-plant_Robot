using Newtonsoft.Json;
using SolarServer.Commands;
using SolarServer.Data;
using SolarServer.Events;
using SolarServer.Extension;
using SolarServer.Models;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SolarServer.Connection
{
    public class ClientSocketHandler
    {
        TcpClient clientSocket;
        StreamReader reader { set; get; }
        StreamWriter writer { set; get; }
        Thread ctThread;
        Thread checkConnectionThread;
        bool isFail = false;
        public void startClient(StreamReader reader, StreamWriter writer,TcpClient inClientSocket)
        {
            this.reader = reader;
            this.writer = writer;
            clientSocket = inClientSocket;
            ctThread = new Thread(run);
            ctThread.Start();
            checkConnectionThread = new Thread(checkConnection);
            checkConnectionThread.Start();
        }
        public void Send(BaseCommand cmd)
        {
            if (isFail)
            {
                return;
            }
            try
            {
                if (writer != null)
                {

                    writer.WriteLine(JsonConvert.SerializeObject(cmd));

                }
            } catch
            {
                isFail = true;
            }
          
        }
        private void checkConnection()
        {
            try
            {
                while (true) { 

                    if (clientSocket != null)
                    {
                        if (!clientSocket.Client.IsConnected())
                        {
                            Console.WriteLine("Machine disconnected");
                            ClientManager.Instance().RemoveMachine(this);

                            break;
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {

            }
        }
        public void Stop()
        {
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<MachineDisconnectedEvent>().Publish();
            try
            {
                if (checkConnectionThread != null)
                {
                    checkConnectionThread.Abort();
                }
            }
            catch
            {
                
            }
            try
            {
                reader.Close();
                writer.Close();
            }
            catch
            {

            }
            try
            {
                clientSocket.Close();
            }
            catch
            {

            }
            try
            {
                
                if (ctThread != null)
                {
                    ctThread.Abort();
                }
            }
            catch
            {
               
            }
        }
        private void run()
        {
            NetworkStream networkStream = clientSocket.GetStream();
            writer.AutoFlush = true;
            while (true)
            {
                try
                {
                    while (true)
                    {
                        string str = reader.ReadLine();

                       // ApplicationService.Instance().eventAggregator.GetEvent<LogEvent>().Publish(">> Received from client" + clNo + ": " + str);
                        var cmd = JsonConvert.DeserializeObject<BaseCommand>(str);
                        var realCmd = BaseCommand.GetCommand(cmd);
                        if (realCmd.Code == CommandCode.GET_MACHINE_LIST)
                        {
                            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Client "+realCmd.SendID+" ask for machine list","Client"));
                            realCmd.IsSuccess = true;
                            realCmd.SecondParameter = JsonConvert.SerializeObject(MachineManager.Instance().MachineList);
                            Send(realCmd);
                        }
                        else if (realCmd.Code == CommandCode.SET_PARAMETER_COMMAND)
                        {
                            realCmd.Exec();
							realCmd.Code = CommandCode.CMD_RESULT;
							realCmd.IsSuccess = true;
							realCmd.SecondParameter = realCmd.CmdToken;
							Send(realCmd);
						}
                        else if (realCmd.Code == CommandCode.INITILIZE_COMMAND)
                        {
                            realCmd.Exec();
							realCmd.Code = CommandCode.CMD_RESULT;
							realCmd.IsSuccess = true;
							realCmd.SecondParameter = realCmd.CmdToken;
							Send(realCmd);
						}
                        else if (realCmd.Code== CommandCode.SET_MODE_COMMAND)
                        {
                            realCmd.Exec();
							realCmd.Code = CommandCode.CMD_RESULT;
							realCmd.IsSuccess = true;
							realCmd.SecondParameter = realCmd.CmdToken;
							Send(realCmd);
						}
                        else if (realCmd.Code == CommandCode.GET_PLANTMAP_COMMAND)
                        {
                            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Client " + realCmd.SendID + " get plant map", "Client"));
                            realCmd.IsSuccess = true;
                            realCmd.SecondParameter = JsonConvert.SerializeObject(RowOfStringManager.Instance.RowOfPanelList);
                            Send(realCmd);
                        }
                        else
                        {
                            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new ErrorLogModel("Unknown command from client: " + realCmd.SendID, "Client"));
                          
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                    break;
                }
            }
        }
    }
}

