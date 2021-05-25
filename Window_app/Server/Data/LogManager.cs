using Prism.Events;
using SolarServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Data
{
    class LogManager
    {
        private static LogManager _instance;
        public EventAggregator eventAggregator;
        private LogManager()
        {
            Logs = new ObservableCollection<LogModel>();
        }
        public static LogManager Instance()
        {
            if (_instance == null)
            {
                _instance = new LogManager();
            }
            return _instance;
        }
        private ObservableCollection<LogModel> _logs;
        public ObservableCollection<LogModel> Logs
        {
            set
            {
                _logs = value;
            }
            get
            {
                return _logs;
            }
        }

    }
}
