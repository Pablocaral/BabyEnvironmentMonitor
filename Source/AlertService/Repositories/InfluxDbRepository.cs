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
        private string TopicBaseName {get; set;}
        
        public InfluxDbRepository(IConfiguration configuration)
        {
            var endPoint = configuration.GetValue<string>("InlfuxDB:Endpoint");
            var user = configuration.GetValue<string>("InlfuxDB:User");
            var password = configuration.GetValue<string>("InlfuxDB:Password");
            
            client = new InfluxDBClient(endPoint, user, password);
            
            Measurement = configuration.GetValue<string>("InlfuxDB:Measurement");
            DatabaseName = configuration.GetValue<string>("InlfuxDB:DatabaseName");

            TopicBaseName = "esi/prototype/";
        }

        public float? GetLastSensorValue(string sensor)
        {
            var task = client.QueryMultiSeriesAsync(DatabaseName, $"SELECT LAST(value) FROM {Measurement} " + 
                $" where topic='{TopicBaseName}{sensor}'");
            task.Wait();
            var item = task.Result.FirstOrDefault();
            
            if(item != default && item.HasEntries)
            {
                return float.Parse(item.Entries.First().Last);
            }

            return default;
        }
    }
}