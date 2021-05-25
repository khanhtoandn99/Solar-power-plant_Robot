using Prism.Events;
namespace SolarServer.Data
{
    public class ApplicationService
    {
        private static ApplicationService _instance;
        public EventAggregator CurrentEventAggregator; 
        private ApplicationService()
        {
            CurrentEventAggregator = new EventAggregator();
        }
        public static ApplicationService Instance()
        {
            if (_instance==null) {
                _instance = new ApplicationService();
            }
            return _instance;
        }
            
     }
}
