using Google.Protobuf.WellKnownTypes;

namespace Com.EnvironmentDataApi.InfluxModels
{
    public class CradleLight
    {
        public string SensorId {get; set;}
        public string SenvironmentId {get; set;}
        public int Lumens {get; set;}
        public Timestamp Time {get; set;}
    }
}