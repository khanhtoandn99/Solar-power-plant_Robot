using Newtonsoft.Json;
using SolarServer.Commands;
using SolarServer.Data;
using SolarServer.Events;
using SolarServer.Extension;
using SolarServer.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using MachineStatus = SolarServer.Models.MachineStatus;

namespace SolarServer.Connection
{
    public class MachineSocketHandler
    {
        TcpClient clientSocket;
        string clientID;
        Thread cmdThread;
        Thread checkConnectionThread;
        StreamReader reader { set; get; }
        StreamWriter writer { set; get; }
        public void startClient(StreamReader reader, StreamWriter writer, TcpClient inClientSocket)
        {
            this.reader = reader;
            this.writer = writer;
            this.clientSocket = inClientSocket;
            cmdThread = new Thread(run);
            checkConnectionThread = new Thread(checkConnection);
            cmdThread.Start();
            checkConnectionThread.Start();
        }
        public void Send(BaseCommand cmd)
        {
            if (writer != null)
            {

                writer.WriteLine(JsonConvert.SerializeObject(cmd));

            }
        }
        public void SendRawCmd(string cmd)
        {
            if (writer != null)
            {
                Console.WriteLine("Send raw cmd to machine: " + cmd);
                var machine = MachineManager.Instance().MachineList.FirstOrDefault(item => item.CommandConnectionHandler == this);
                if (machine != null)
                {
                    MachineManager.Instance().AddLog(machine, new MachineLog(cmd, "Sent CMD"));

                }
                writer.WriteLine(cmd);

            }
        }
        private void checkConnection()
        {
            try
            {
                while (true)
                {

                    if (clientSocket != null)
                    {
                        if (!clientSocket.Client.IsConnected())
                        {
                            Console.WriteLine("Machine disconnected");
                            MachineManager.Instance().RemoveMachine(this);

                            break;
                        }

                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(2000);
                }
            }
            catch
            {

            }
        }
        public void Stop()
        {
           
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
                if (cmdThread != null)
                {
                    cmdThread.Abort();

                }

            }
            catch
            {

            }
           // MachineManager.Instance().RemoveMachine(this);
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<MachineDisconnectedEvent>().Publish();
        }
        private void run()
        {
            NetworkStream networkStream = clientSocket.GetStream();
            writer.AutoFlush = true;
            int retryAtempt = 0;

            while (true)
            {
                try
                {
                    string str = reader.ReadLine();
                    // Console.WriteLine(str);
                    var machineTmp = MachineManager.Instance().MachineList.FirstOrDefault(item => item.CommandConnectionHandler == this);
                    if (machineTmp != null)
                    {
                        MachineManager.Instance().AddLog(machineTmp, new MachineLog(str, "Received CMD"));
                    }
                    // ApplicationService.Instance().eventAggregator.GetEvent<LogEvent>().Publish(">> Received from machine" + clNo + ": " + str);
                    var cmd = JsonConvert.DeserializeObject<RawCommand>(str);
                    //  var realCmd = BaseCommand.GetCommand(cmd);
                   
                    if (cmd.Type == 30)
                    {
                        var updateCMD = new UpdateMachineStatus();
                        updateCMD.Code = CommandCode.UPDATE_MACHINE_STATUS;
                        var battery = JsonConvert.DeserializeObject<BatteryModel>(cmd.Data);
                        var tmpMachine = MachineManager.Instance().MachineList.FirstOrDefault(machine => machine.CommandConnectionHandler == this);
                        if (tmpMachine != null)
                        {
                            tmpMachine.BatteryPercent = battery.energy.ToString();
                            tmpMachine.Volt = battery.voltage.ToString();
                            tmpMachine.AMP = battery.current.ToString();
                            updateCMD.SecondParameter = JsonConvert.SerializeObject(tmpMachine);
                            ClientManager.Instance().BroadCast(updateCMD);
                            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Machine " + tmpMachine.IDRobot + " updated its batery status", "Machine"));
                        }

                    }
                    else if (cmd.Type == 32)
                    {
                        // ApplicationService.Instance().eventAggregator.GetEvent<LogEvent>().Publish(">> Machine update");
                        // realCmd.Exec();
                        var battery = JsonConvert.DeserializeObject<BatteryModel>(cmd.Data);
                        var tmpMachine = MachineManager.Instance().MachineList.FirstOrDefault(machine => machine.CommandConnectionHandler == this);
                        if (tmpMachine != null)
                        {
                            tmpMachine.BatteryPercent = battery.energy.ToString();
                            tmpMachine.Volt = battery.voltage.ToString();
                            tmpMachine.AMP = battery.current.ToString();
                        }
                    }
                    else if (cmd.Type == 31)
                    {
                        var status = JsonConvert.DeserializeObject<MachineStatus>(cmd.Data);
                        var tmpMachine = MachineManager.Instance().MachineList.FirstOrDefault(machine => machine.CommandConnectionHandler == this);
                        if (tmpMachine != null && status != null)
                        {
                            var updateCMD = new BaseCommand();
                            updateCMD.Code = CommandCode.UPDATE_PLANT_MAP_POSITION_COMMAND;
                            tmpMachine.Status = status.status.ToString();
                            var location = new Position();
                            location.X = status.collumn;
                            location.Y = status.row;
                            tmpMachine.Position = location;
                            tmpMachine.Direction = status.direction.ToString();
                            tmpMachine.Status = status.status;
                            updateCMD.SecondParameter = JsonConvert.SerializeObject(tmpMachine);
                            RowOfStringManager.Instance.UpdatePostion(tmpMachine);
                            ClientManager.Instance().BroadCast(updateCMD);
                            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Machine " + tmpMachine.IDRobot + " updated its status", "Machine"));

                        }
                    }
                    else
                    {
                        var tmpMachine = MachineManager.Instance().MachineList.FirstOrDefault(machine => machine.CommandConnectionHandler == this);
                        if (tmpMachine == null)
                        {
                            return;
                        }
                        ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new ErrorLogModel("Unknown command from machine: " + tmpMachine.IDRobot, "Machine"));
                    }
                    retryAtempt = 0;
                }
                catch (Exception ex)
                {
                    if (retryAtempt == 5)
                    {

                        break;
                    }
                    else
                    {
                        Thread.Sleep(500);
                        retryAtempt++;
                    }
                }
            }
        }
    }
}
