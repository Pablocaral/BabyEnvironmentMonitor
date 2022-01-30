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
        
        public EnvironmentState GetCurrentState(NancyContext context, string environmentUid)
        {
            try
            {
                return getEnvironmentData(environmentUid).GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                log.Error("An error ocurred on GetCo2History operation", ex);
                return null;
            }
        }

        public async Task<EnvironmentState> getEnvironmentData(String environmentUid)
        {
            String petition = "select * from environmentData where environmentId="+environmentUid;
            var query = await Client.QueryMultiSeriesAsync("environmentData", petition); 

            var state = query.ElementAt(query.Count-1).Entries[0];
            var actualState = new EnvironmentState () {
                CO2 = Convert.ToDecimal(state.Co2),
                Humidity = Convert.ToDecimal(state.Humidity),
                Light = Convert.ToDecimal(state.Light),
                Noise = Convert.ToDecimal(state.Noise),
                Temperature = Convert.ToDecimal(state.Temperature)
            };

            return actualState;
        }
    }
}