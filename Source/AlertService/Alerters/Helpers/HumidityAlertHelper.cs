using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Alerters.Helpers
{
    public class HumididtyAlertHelper
    {
        private readonly IConfiguration configuration;

        public HumididtyAlertHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsOutOfHighAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:HighAlerts:Humidity:UpperLimit") ||
                currentCo2Value <= configuration.GetValue<int>("ThresholdValues:HighAlerts:Humidity:LowerLimit");
        }
        public bool IsOutOfMediumAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:MediumAlerts:Humidity:UpperLimit") ||
                currentCo2Value <= configuration.GetValue<int>("ThresholdValues:MediumAlerts:Humidity:LowerLimit");
        }
    }
}