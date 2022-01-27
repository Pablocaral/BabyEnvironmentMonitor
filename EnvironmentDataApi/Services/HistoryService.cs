using System;
using System.Reflection;
using Com.EnvironmentDataApi.NancyModels;
using Com.EnvironmentDataApi.NancyModules;
using log4net;
using Nancy;

namespace Com.EnvironmentDataApi.Services
{
    public class HistoryService : IHistoryService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public Co2History GetCo2History(NancyContext context, string environmentUid)
        {
            try
            {
                // ToDo
                throw new System.NotImplementedException();
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetCo2History operation", ex);
                return null;
            }
        }

        public HumidityHistory GetHumidityHistory(NancyContext context, string environmentUid)
        {
            try
            {
                // ToDo
                throw new System.NotImplementedException();
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetHumidityHistory operation", ex);
                return null;
            }
        }

        public LightHistory GetLightHistory(NancyContext context, string environmentUid)
        {
            try
            {
                // ToDo
                throw new System.NotImplementedException();
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetLightHistory operation", ex);
                return null;
            }
        }

        public NoiseHistory GetNoiseHistory(NancyContext context, string environmentUid)
        {
            try
            {
                // ToDo
                throw new System.NotImplementedException();
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetNoiseHistory operation", ex);
                return null;
            }
        }

        public TemperatureHistory GetTemperatureHistory(NancyContext context, string environmentUid)
        {
            try
            {
                // ToDo
                throw new System.NotImplementedException();
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetTemperatureHistory operation", ex);
                return null;
            }
        }
    }
}