using Google.Protobuf.WellKnownTypes;

namespace Com.EnvironmentDataApi.InfluxModels
{
    public class CradleCo2Model
    {
        public string SensorId {get; set;}
        public string EnvironmentId {get; set;}
        public int Co2Ppm {get; set;}
        public Timestamp Time {get; set;}
    }
}