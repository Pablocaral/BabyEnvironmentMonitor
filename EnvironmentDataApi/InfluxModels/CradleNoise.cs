using Google.Protobuf.WellKnownTypes;

namespace Com.EnvironmentDataApi.InfluxModels
{
    public class CradleNoise
    {
        public string SensorId {get; set;}
        public string SenvironmentId {get; set;}
        public int Decibels  {get; set;}
        public Timestamp Time {get; set;}
    }
}