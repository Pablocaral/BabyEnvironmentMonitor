using System.Reflection;
using Com.EnvironmentDataApi.NancyModules;
using Com.EnvironmentDataApi.NancyModels;
using log4net;
using Nancy;
using System;

namespace Com.EnvironmentDataApi.Services
{
    public class StateService : IStateService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public EnvironmentState GetCurrentState(NancyContext context, string environmentUid)
        {
            try
            {
                // ToDo
                return new EnvironmentState()
                {
                    CO2 = 1,
                    Humidity = 1,
                    Light = 1,
                    Noise = 1,
                    Temperature = 1
                };
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetCo2History operation", ex);
                return null;
            }
        }
    }
}