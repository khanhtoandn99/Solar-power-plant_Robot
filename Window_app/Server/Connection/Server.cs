using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;
using SolarServer.Data;
using SolarServer.Events;
using SolarServer.Connection;
using SolarServer.Models;

namespace SolarServer.Utils
{

    public class Server
    {
        public int Port { set; get; }
        public string IP { set; get; }
        private TcpListener _socketListener { set; get; }
        private Server() { }
        private Server(int port)
        {
            if (port<=0)
            {
                throw new ArgumentException();
            }
            Port = port;
            _socketListener = new TcpListener(port);
            TcpClient clientSocket = default(TcpClient);

        }
        private Server(string address, int port)
        {
            if (port <= 0)
            {
                throw new ArgumentException();
            }
            Port = port;
            var ipAddress = new IPAddress(Encoding.ASCII.GetBytes(address));
            _socketListener = new TcpListener(ipAddress,port);
            TcpClient clientSocket = default(TcpClient);

        }
        public static Server On(int port)
        {
            return new Server(port);
        }
        public static Server On(string ip, int port)
        {
            return new Server(ip, port);
        }
        public void Run()
        {
            _socketListener.Start();
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<ServerStatusEvent>().Publish(true);
            ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Server is running", "Server"));
            try
            {
                while (true)
                {
                    var tcpClient = _socketListener.AcceptTcpClient();
                    UniversalSocketHandler client = new UniversalSocketHandler();
                    client.startClient(tcpClient);
                }
            } catch
            {
                ApplicationService.Instance().CurrentEventAggregator.GetEvent<ServerStatusEvent>().Publish(false);
            }
        }
        public void Stop()
        {
            lock (_socketListener)
            {
                if (_socketListener != null)
                {
                    try
                    {

                        MachineManager.Instance().Stop();
                    } catch
                    {

                    }
                    try
                    {
                        ClientManager.Instance().Stop();
                    }
                    catch
                    {

                    }
                    _socketListener.Stop();
                    
                    ApplicationService.Instance().CurrentEventAggregator.GetEvent<ServerStatusEvent>().Publish(false);
                    ApplicationService.Instance().CurrentEventAggregator.GetEvent<LogEvent>().Publish(new InfoLogModel("Server is stop!","Server"));

                }
            }
          
        }

    }
}