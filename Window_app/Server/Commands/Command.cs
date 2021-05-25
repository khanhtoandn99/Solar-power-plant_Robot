using Newtonsoft.Json;

namespace SolarServer.Commands
{
    public class BaseCommand
    {
        public int SocketClientType { set; get; }
        public bool IsSuccess { set; get; }
        public int Code { set; get; }
        public string TargetID { set; get; }
        public string SendID { set; get; }
        public string CmdToken { set; get; }
        public string FirstParameter { set; get; }
        public string SecondParameter { set; get; }
        public virtual void Exec() {
        }
        public void OnFail()
        {

        }
        public T GetData<T>()
        {
            return JsonConvert.DeserializeObject<T>(SecondParameter);
        }
        public static BaseCommand GetCommand(BaseCommand cmd)
        {
            if (cmd.Code == CommandCode.CMD_RESULT)
            {
                return new RegisterCommand();
            }
            else if (cmd.Code == CommandCode.SET_PARAMETER_COMMAND)
            {
                var serializedParent = JsonConvert.SerializeObject(cmd);
                return JsonConvert.DeserializeObject<SetParameterCommand>(serializedParent);
            } else if (cmd.Code == CommandCode.INITILIZE_COMMAND)
            {
                var serializedParent = JsonConvert.SerializeObject(cmd);
                return JsonConvert.DeserializeObject<InitializeCommand>(serializedParent);
            } else if (cmd.Code == CommandCode.SET_MODE_COMMAND)
            {
                var serializedParent = JsonConvert.SerializeObject(cmd);
                return JsonConvert.DeserializeObject<SetModeCommand>(serializedParent);
            }
         
           
            /*
            else if (cmd.Code == CommandCode.NEW_MACHINE_EVENT)
            {
                return new NewMachineClientCommand();
            }
            else if (cmd.Code == CommandCode.MACHINE_LOGOUT)
            {
                return new MachineLogoutCommand();
            }
            */
            else
            {
                return cmd;
            }
        }
    }
}
