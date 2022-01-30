using System.Collections.Generic;
using System;
using System.Reflection;
using Com.EnvironmentDataApi.NancyModels;
using Com.EnvironmentDataApi.NancyModules;
using log4net;
using Nancy;
using AdysTech.InfluxDB.Client.Net;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace Com.EnvironmentDataApi.Services
{
    public class HistoryService : IHistoryService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public IConfiguration configuration {get; set;}

        private InfluxDBClient _client;
        private InfluxDBClient Client
        {
            get
            {
                if(_client == null)
                {
                    var endPoint = configuration.GetValue<string>("InlfuxDB:Endpoint");
                    var user = configuration.GetValue<string>("InlfuxDB:User");
                    var password = configuration.GetValue<string>("InlfuxDB:Password");
                    
                    _client = new InfluxDBClient(endPoint, user, password);
                }

                return _client;
            }   
        }
        
        public Co2History GetCo2History(NancyContext context, string environmentUid)
        {
            try
            {
                DateTime startPeriod = DateTime.Now.AddDays(-7);
                DateTime endPeriod = DateTime.Now;

                var period = new Com.EnvironmentDataApi.NancyModels.Period(startPeriod,endPeriod);
                List<float?> elements = GetHistory(startPeriod,endPeriod,"co2",environmentUid).GetAwaiter().GetResult();
                
                return new Co2History (period,3600,elements);
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
                DateTime startPeriod = DateTime.Now.AddDays(-7);
                DateTime endPeriod = DateTime.Now;

                var period = new Com.EnvironmentDataApi.NancyModels.Period(startPeriod,endPeriod);
                List<float?> elements = GetHistory(startPeriod,endPeriod,"humidity",environmentUid).GetAwaiter().GetResult();
                
                return new HumidityHistory (period,3600,elements);
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
                DateTime startPeriod = DateTime.Now.AddDays(-7);
                DateTime endPeriod = DateTime.Now;

                var period = new Com.EnvironmentDataApi.NancyModels.Period(startPeriod,endPeriod);
                List<float?> elements = GetHistory(startPeriod,endPeriod,"light",environmentUid).GetAwaiter().GetResult();
                
                return new LightHistory (period,3600,elements);
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
                DateTime startPeriod = DateTime.Now.AddDays(-7);
                DateTime endPeriod = DateTime.Now;

                var period = new Com.EnvironmentDataApi.NancyModels.Period(startPeriod,endPeriod);
                List<float?> elements = GetHistory(startPeriod,endPeriod,"noise",environmentUid).GetAwaiter().GetResult();
                
                return new NoiseHistory (period,3600,elements);
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
                DateTime startPeriod = DateTime.Now.AddDays(-7);
                DateTime endPeriod = DateTime.Now;

                var period = new Com.EnvironmentDataApi.NancyModels.Period(startPeriod,endPeriod);
                List<float?> elements = GetHistory(startPeriod,endPeriod,"temperature",environmentUid).GetAwaiter().GetResult();
                
                return new TemperatureHistory (period,3600,elements);
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetTemperatureHistory operation", ex);
                return null;
            }
        }

        public async Task<List<float?>> GetHistory(DateTime startPeriod,DateTime endPeriod, String variable, String environmentUid)
        {
            string formatEndPeriod = endPeriod.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string formatStartPeriod = startPeriod.ToString("yyyy-MM-ddTHH:mm:ssZ");
            
            
            String petition = "select mean("+variable+") from environmentData where environmentId="+environmentUid+" and time>='"+formatStartPeriod+"' and time<='"+formatEndPeriod+"' group by time(1h)";
            var query = await Client.QueryMultiSeriesAsync("environmentData", petition);

            List<float?> hours = new List<float?>();
            foreach(var s in query){
                foreach(var entrie in s.Entries){
                    log.Debug(entrie);
                    log.Debug(entrie.Mean);
                    if(entrie.Mean == null){
                        hours.Add(0);
                    }else{
                        hours.Add(float.Parse(entrie.Mean,CultureInfo.InvariantCulture.NumberFormat));
                    }
                }
            }
            
            return hours;
        }
    }
}