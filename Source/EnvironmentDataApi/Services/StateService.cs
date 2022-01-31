using System.Globalization;
using System.Linq;
using System.Reflection;
using Com.EnvironmentDataApi.NancyModules;
using Com.EnvironmentDataApi.NancyModels;
using log4net;
using Nancy;
using System;
using AdysTech.InfluxDB.Client.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Com.EnvironmentDataApi.Services
{
    public class StateService : IStateService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
        private InfluxDBClient client;

        private string DatabaseName {get; set;}
        private string Measurement {get; set;}
        private string TopicBaseName {get; set;}
        
        public StateService(IConfiguration configuration)
        {
            var endPoint = configuration.GetValue<string>("InlfuxDB:Endpoint");
            var user = configuration.GetValue<string>("InlfuxDB:User");
            var password = configuration.GetValue<string>("InlfuxDB:Password");

            client = new InfluxDBClient(endPoint, user, password);

            Measurement = configuration.GetValue<string>("InlfuxDB:Measurement");
            DatabaseName = configuration.GetValue<string>("InlfuxDB:DatabaseName");

            TopicBaseName = "esi/prototype/";
        }

        public EnvironmentState GetCurrentState(NancyContext context, string environmentUid)
        {
            try
            {
                return GetEnvironmentData(environmentUid);
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetCo2History operation", ex);
                return null;
            }
        }

        private EnvironmentState GetEnvironmentData(String environmentUid)
        {
            var currentState = new EnvironmentState () {
                CO2 = LoadLastRegistryValue(environmentUid, "co2"),
                Humidity = LoadLastRegistryValue(environmentUid, "humidity"),
                Light = LoadLastRegistryValue(environmentUid, "light"),
                Noise = LoadLastRegistryValue(environmentUid, "noise"),
                Temperature = LoadLastRegistryValue(environmentUid, "temperature"),
            };

            return currentState;
        }

        private decimal? LoadLastRegistryValue(string environmentUid, string sensor)
        {
            var query = $"select LAST(value) from {Measurement} "
                +$"where environmentId={environmentUid} and topic='{TopicBaseName}{sensor}'";
            
            var task = client.QueryMultiSeriesAsync(DatabaseName, query);
            task.Wait();
            
            var registry = task.Result.FirstOrDefault();
            if(registry != default  && registry.HasEntries)
            {
                return decimal.Parse(registry.Entries.First().Last, CultureInfo.InvariantCulture);
            }

            return null;
        }
    }
}