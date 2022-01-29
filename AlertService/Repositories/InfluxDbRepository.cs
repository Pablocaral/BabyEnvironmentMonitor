using System.Linq;
using AdysTech.InfluxDB.Client.Net;
using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Repositories
{
    public class InfluxDbRepository
    {
        private readonly InfluxDBClient client;
        
        public InfluxDbRepository(IConfiguration configuration)
        {
            var endPoint = configuration.GetValue<string>("InlfuxDB:Endpoint");
            var user = configuration.GetValue<string>("InlfuxDB:User");
            var password = configuration.GetValue<string>("InlfuxDB:Password");

            client = new InfluxDBClient(endPoint, user, password);
        }

        public int? GetLastCo2Value()
        {
            var task = client.QueryMultiSeriesAsync("baby_environment", "SELECT LAST(*) FROM cradleCo2");
            task.Wait();
            var item = task.Result.FirstOrDefault();
            
            if(item != default && item.HasEntries)
            {
                return int.Parse(item.Entries.First().Last_co2Ppm);
            }

            return default;
        }
    }
}