using System.Collections.Generic;
using System;
using System.Reflection;
using Com.EnvironmentDataApi.NancyModels;
using Com.EnvironmentDataApi.NancyModules;
using log4net;
using Nancy;
using AdysTech.InfluxDB.Client.Net;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace Com.EnvironmentDataApi.Services
{
    public class HistoryService : IHistoryService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private InfluxDBClient client;

        private string DatabaseName {get; set;}
        private string Measurement {get; set;}
        private string TopicBaseName {get; set;}

        public HistoryService(IConfiguration configuration)
        {
            var endPoint = configuration.GetValue<string>("InlfuxDB:Endpoint");
            var user = configuration.GetValue<string>("InlfuxDB:User");
            var password = configuration.GetValue<string>("InlfuxDB:Password");

            client = new InfluxDBClient(endPoint, user, password);

            Measurement = configuration.GetValue<string>("InlfuxDB:Measurement");
            DatabaseName = configuration.GetValue<string>("InlfuxDB:DatabaseName");

            TopicBaseName = "esi/prototype/";
        }
        
        public Co2History GetCo2History(NancyContext context, string environmentUid)
        {
            try
            {
                DateTime startPeriod = DateTime.Now.AddDays(-7);
                DateTime endPeriod = DateTime.Now;

                var period = new Com.EnvironmentDataApi.NancyModels.Period(startPeriod,endPeriod);
                List<float?> elements = GetHistory(startPeriod,endPeriod,"co2",environmentUid);

                Co2History sendRequest = null;
                if( elements.Count > 0) {
                    sendRequest = new Co2History (period,3600,elements);
                } 

                return sendRequest;
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
                List<float?> elements = GetHistory(startPeriod,endPeriod,"humidity",environmentUid);
                
                HumidityHistory sendRequest = null;
                if( elements.Count > 0) {
                    sendRequest = new HumidityHistory (period,3600,elements);
                } 

                return sendRequest;
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
                List<float?> elements = GetHistory(startPeriod,endPeriod,"light",environmentUid);
                
                LightHistory sendRequest = null;
                if( elements.Count > 0) {
                    sendRequest = new LightHistory (period,3600,elements);
                } 

                return sendRequest;
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
                List<float?> elements = GetHistory(startPeriod,endPeriod,"noise",environmentUid);
                
                NoiseHistory sendRequest = null;
                if( elements.Count > 0) {
                    sendRequest = new NoiseHistory (period,3600,elements);
                } 

                return sendRequest;
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
                List<float?> elements = GetHistory(startPeriod,endPeriod,"temperature",environmentUid);
                
                TemperatureHistory sendRequest = null;
                if( elements.Count > 0) {
                    sendRequest = new TemperatureHistory (period,3600,elements);
                } 

                return sendRequest;
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetTemperatureHistory operation", ex);
                return null;
            }
        }

        public List<float?> GetHistory(DateTime startPeriod,DateTime endPeriod, string variable, string environmentUid)
        {
            string formatEndPeriod = endPeriod.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string formatStartPeriod = startPeriod.ToString("yyyy-MM-ddTHH:mm:ssZ");
            
            string petition = $"select mean(value) from {Measurement} where environmentId={environmentUid} " +
                $"and time>='{formatStartPeriod}' and time<='{formatEndPeriod}' " + 
                $"and topic='{TopicBaseName}{variable}' group by time(1h)";
            
            var task = client.QueryMultiSeriesAsync(DatabaseName, petition);
            task.Wait();
            var query = task.Result;

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