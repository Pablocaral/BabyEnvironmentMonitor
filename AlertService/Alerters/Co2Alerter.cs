using System.Threading;
using System.Threading.Tasks;
using Com.AlertService.Notifiers;
using Com.AlertService.Repositories;
using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Alerters
{
    public class Co2Alerter : IAlerter
    {
        private readonly IConfiguration configuration;
        private readonly InfluxDbRepository influxDbRepository;
        private readonly INotifier notifier;

        public Co2Alerter(IConfiguration configuration, 
            InfluxDbRepository influxDbRepository,
            INotifier notifier)
        {
            this.configuration = configuration;
            this.influxDbRepository = influxDbRepository;
            this.notifier = notifier;
        }

        public Task Run(CancellationToken cancellationToken)
        {
            return Task.Run(() => 
            {
                while(!cancellationToken.IsCancellationRequested)
                {
                    var currentCo2Value = influxDbRepository.GetLastCo2Value();
                    if(currentCo2Value != default)
                    {
                        if(IsOutOfHighAlertRange((int)currentCo2Value))
                        {
                            notifier.Notify((double)currentCo2Value, "CO2", AlertType.High);
                        }
                        else if(IsOutOfMediumAlertRange((int)currentCo2Value))
                        {
                            notifier.Notify((double)currentCo2Value, "CO2", AlertType.Medium);
                        }
                    }

                    Thread.Sleep(configuration.GetValue<int>("AlertersDelayMs"));
                }
            });
        }

        private bool IsOutOfHighAlertRange(int currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:HighAlerts:UpperLimit") ||
                currentCo2Value <= configuration.GetValue<int>("ThresholdValues:HighAlerts:LowerLimit");
        }
        private bool IsOutOfMediumAlertRange(int currentCo2Value)
        {
            return currentCo2Value >= configuration.GetValue<int>("ThresholdValues:MediumAlerts:UpperLimit") ||
                currentCo2Value <= configuration.GetValue<int>("ThresholdValues:MediumAlerts:LowerLimit");
        }
    }
}