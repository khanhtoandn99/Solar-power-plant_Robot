using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarServer.Data
{
    class CommandCode
    {
        public const int NEW_MACHINE_EVENT = 1;
        public const int GET_MACHINE_LIST = 2;
        public const int MACHINE_LOGOUT = 3;
        public const int CMD_RESULT = 4;
        public const int REGISTER_CLIENT = 5;
        public const int CLIENT_LOGOUT = 8;
        public const int SET_PARAMETER_COMMAND = 7;
        public const int SET_MODE_COMMAND = 6;
        public const int UPDATE_MACHINE_STATUS = 9;
        public const int INITILIZE_COMMAND = 10;
    }
}
