using System.Reflection;
using Com.EnvironmentDataApi.Modules;
using Com.EnvironmentDataApi.Models;
using log4net;
using Nancy;

namespace Com.EnvironmentDataApi.Services
{
    public class StateService : IStateService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public EnvironmentState GetCurrentState(NancyContext context, string environmentUid)
        {
            log.Info("Processing Get Current State operation.");
            return new EnvironmentState()
            {
                CO2 = 1,
                Humidity = 1,
                Light = 1,
                Noise = 1,
                Temperature = 1
            };
        }
    }
}