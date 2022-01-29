using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Alerters.Helpers
{
    public class LightAlertHelper
    {
        private readonly IConfiguration configuration;

        public LightAlertHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsOutOfHighAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:HighAlerts:Light:UpperLimit");
        }
        public bool IsOutOfMediumAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:MediumAlerts:Light:UpperLimit");
        }
    }
}