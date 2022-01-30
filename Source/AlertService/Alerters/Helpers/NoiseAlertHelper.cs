using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Alerters.Helpers
{
    public class NoiseAlertHelper
    {
        private readonly IConfiguration configuration;

        public NoiseAlertHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsOutOfHighAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:HighAlerts:Noise:UpperLimit");
        }
        public bool IsOutOfMediumAlertRange(float currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:MediumAlerts:Noise:UpperLimit");
        }
    }
}