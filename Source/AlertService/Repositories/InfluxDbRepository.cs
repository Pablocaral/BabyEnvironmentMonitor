using System.Linq;
using AdysTech.InfluxDB.Client.Net;
using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Repositories
{
    public class InfluxDbRepository
    {
        private readonly InfluxDBClient client;
        
        private string DatabaseName {get; set;}
        private string Measurement {get; set;}
        
        public InfluxDbRepository(IConfiguration configuration)
        {
            var endPoint = configuration.GetValue<string>("InlfuxDB:Endpoint");
            var user = configuration.GetValue<string>("InlfuxDB:User");
            var password = configuration.GetValue<string>("InlfuxDB:Password");
            
            client = new InfluxDBClient(endPoint, user, password);
            
            Measurement = configuration.GetValue<string>("InlfuxDB:Measurement");
            DatabaseName = configuration.GetValue<string>("InlfuxDB:DatabaseName");
        }

        public EnvironmentDataModel GetLastEnvironmentState()
        {
            var task = client.QueryMultiSeriesAsync(DatabaseName, $"SELECT LAST(*) FROM {Measurement}");
            task.Wait();
            var item = task.Result.FirstOrDefault();
            
            if(item != default && item.HasEntries)
            {
                return BuildEnvironmentDataModel(item.Entries.First());                
            }

            return default;
        }

        private EnvironmentDataModel BuildEnvironmentDataModel(dynamic entry)
        {
            var result = new EnvironmentDataModel();
            
            if(entry.Last_co2 != null)
                result.Co2 = float.Parse(entry.Last_co2);

            if(entry.Last_humidity != null)
                result.Humidity = float.Parse(entry.Last_humidity);

            if(entry.Last_light != null)
                result.Light = float.Parse(entry.Last_light);

            if(entry.Last_temperature != null)
                result.Temperature = float.Parse(entry.Last_temperature);

            if(entry.Last_noise != null)
                result.Noise = float.Parse(entry.Last_noise);

            return result;
        }
    }
}