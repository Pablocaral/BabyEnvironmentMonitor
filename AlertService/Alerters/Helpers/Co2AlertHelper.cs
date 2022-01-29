using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Alerters.Helpers
{
    public class Co2AlertHelper
    {
        private readonly IConfiguration configuration;

        public Co2AlertHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsOutOfHighAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:HighAlerts:Co2:UpperLimit");
        }
        public bool IsOutOfMediumAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:MediumAlerts:Co2:UpperLimit");
        }
    }
}