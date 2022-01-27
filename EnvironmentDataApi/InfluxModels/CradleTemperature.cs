using Google.Protobuf.WellKnownTypes;

namespace Com.EnvironmentDataApi.InfluxModels
{
    public class CradleTemperature
    {
        public string SensorId {get; set;}
        public string SenvironmentId {get; set;}
        public double Celsius {get; set;}
        public Timestamp Time {get; set;}
    }
}