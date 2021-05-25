
using Newtonsoft.Json;
using SolarServer.Commands;
using SolarServer.Data;
using SolarServer.Events;
using SolarServer.Extension;
using SolarServer.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace SolarServer.Connection
{
    public class ImageStreamSocketHandler
    {
        TcpClient clientSocket;
        StreamReader reader { set; get; }
        Thread ctThread;
        Thread checkConnectionThread;
        bool isFail = false;
        String idRobot;
        public void startClient(StreamReader reader, TcpClient inClientSocket, String id)
        {
            this.reader = reader;
            clientSocket = inClientSocket;
            ctThread = new Thread(run);
            ctThread.Start();
            checkConnectionThread = new Thread(checkConnection);
            checkConnectionThread.Start();
            idRobot = id;
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
                            Console.WriteLine("ras disconnected");
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

            try
            {
                while (true)
                    {
               
                    var len = reader.ReadLine();
                        if (len==null)
                    {
                        break;
                    }
                        try
                    {
                        var image = JsonConvert.DeserializeObject<ImagModel>(len);
                        var cmd = new UpdateImageCommand();
                        cmd.Code = CommandCode.UPDATE_IMAGE_COMMAND;
                  
                       
                    var updateImageModel = new
                    {
                        img = image.img.Substring(2, image.img.Length - 3),
                        RobotID = idRobot,
                        ObjectCount = image.ObjectCount,
                        strpanel = image.strpanel,
                        panel = image.panel,
                        isObjectDetected = image.isObjectDetected,
                    };
                    if (updateImageModel.isObjectDetected)
                    {
                        var currentMachine = MachineManager.Instance().MachineList.Where(machine => machine.IDRobot == image.id.Trim()).FirstOrDefault();
                        if (currentMachine!=null)
                        {
                            var log = new MachineLog()
                            {
                                Type = "Error",
                                Content = "Strange object detected: " + image.ObjectCount + " object(s)."
                            };
                            MachineManager.Instance().AddLog(currentMachine, log);
                        }
                    }
                        cmd.SecondParameter = JsonConvert.SerializeObject(updateImageModel);
                    if (image.isObjectDetected)
                    {
                        ClientManager.Instance().BroadCast(cmd, false);
                    } else
                    {
                        ClientManager.Instance().BroadCast(cmd, true);
                    }
                    }
                    catch
                    {

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

