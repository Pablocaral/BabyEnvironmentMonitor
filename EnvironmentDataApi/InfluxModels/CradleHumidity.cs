using Google.Protobuf.WellKnownTypes;

namespace Com.EnvironmentDataApi.InfluxModels
{
    public class CradleHumidity
    {
        public string SensorId {get; set;}
        public string SenvironmentId {get; set;}
        public double HumidityPercentage {get; set;}
        public Timestamp Time {get; set;}
    }
}