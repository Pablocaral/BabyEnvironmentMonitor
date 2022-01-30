using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Alerters.Helpers
{
    public class TemperatureAlertHelper
    {
        private readonly IConfiguration configuration;

        public TemperatureAlertHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsOutOfHighAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:HighAlerts:Temperatre:UpperLimit") ||
                currentCo2Value <= configuration.GetValue<int>("ThresholdValues:HighAlerts:Temperatre:LowerLimit");
        }
        public bool IsOutOfMediumAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:MediumAlerts:Temperatre:UpperLimit") ||
                currentCo2Value <= configuration.GetValue<int>("ThresholdValues:MediumAlerts:Temperatre:LowerLimit");
        }
    }
}