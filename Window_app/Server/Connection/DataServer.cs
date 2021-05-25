using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;
using SolarServer.Data;
using SolarServer.Events;
using SolarServer.Connection;
using SolarServer.Models;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using SolarServer.Commands;
using System.Linq;

namespace SolarServer.Utils
{

    public class DataServer
    {
        public int Port { set; get; }
        public Dispatcher Dispacth { set; get; } 
        public Image ImgControl { set; get; } 
        public string IP { set; get; }
        private TcpListener _socketListener { set; get; }
        private TcpListener _dataSocketListener { set; get; }
        private DataServer() { }
        private DataServer(int port)
        {
            if (port <= 0)
            {
                throw new ArgumentException();
            }
            Port = port;
            _socketListener = new TcpListener(port);
            TcpClient clientSocket = default(TcpClient);
        }
        private DataServer(string address, int port)
        {
            if (port <= 0)
            {
                throw new ArgumentException();
            }
            Port = port;
            var ipAddress = new IPAddress(Encoding.ASCII.GetBytes(address));
            _socketListener = new TcpListener(ipAddress, port);
            TcpClient clientSocket = default(TcpClient);
        }
        public static DataServer On(int port)
        {
            return new DataServer(port);
        }
        public static DataServer On(string ip, int port)
        {
            return new DataServer(ip, port);
        }
        public void Run()
        {
            _socketListener.Start();
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Data Server is running", "Data Server"));

            // while (true)
            // {
            try
            {
                while (true)
                {
                    var tcpClient = _socketListener.AcceptTcpClient();
                    ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("New connection!", "Data Server"));
                    var reader = new StreamReader(tcpClient.GetStream());
                    var robotID = reader.ReadLine();
                    var currentMachine = MachineManager.Instance().MachineList.Where(machine => machine.IDRobot == robotID.Trim()).FirstOrDefault();
                    if (currentMachine!=null)
                    {
                        var client = new ImageStreamSocketHandler();
                        client.startClient(reader, tcpClient, robotID);
                        currentMachine.ImageStreamConnectionHandler = client;
                    } else
                    {
                        tcpClient.Close();
                    }
                    
                } 
              
            }
            catch
            {
            }
           // }
          
        }
      
        public void Stop()
        {
            lock (_socketListener)
            {
                if (_socketListener != null)
                {
                    _socketListener.Stop();
                    ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Data Server is stop!", "Server"));
                }
            }

        }

    }
}