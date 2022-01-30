using System.Threading;
using System.Threading.Tasks;
using Com.AlertService.Alerters.Helpers;
using Com.AlertService.Notifiers;
using Com.AlertService.Repositories;
using Microsoft.Extensions.Configuration;

namespace Com.AlertService.Alerters
{
    public class Alerter
    {
        private readonly IConfiguration configuration;
        private readonly InfluxDbRepository influxDbRepository;
        private readonly INotifier notifier;

        private readonly Co2AlertHelper co2AlertHelper;
        private readonly LightAlertHelper lightAlertHelper;
        private readonly TemperatureAlertHelper temperatureAlertHelper;
        private readonly NoiseAlertHelper noiseAlertHelper;
        private readonly HumididtyAlertHelper humidityAlertHelper;

        public Alerter(IConfiguration configuration,
            InfluxDbRepository influxDbRepository,
            Co2AlertHelper co2AlertHelper,
            LightAlertHelper lightAlertHelper,
            TemperatureAlertHelper temperatureAlertHelper,
            NoiseAlertHelper noiseAlertHelper,
            HumididtyAlertHelper humidityAlertHelper,
            INotifier notifier)
        {
            this.configuration = configuration;
            this.influxDbRepository = influxDbRepository;
            this.notifier = notifier;

            this.co2AlertHelper = co2AlertHelper;
            this.lightAlertHelper = lightAlertHelper;
            this.temperatureAlertHelper = temperatureAlertHelper;
            this.noiseAlertHelper = noiseAlertHelper;
            this.humidityAlertHelper = humidityAlertHelper;
        }

        public Task Run(CancellationToken cancellationToken)
        {
            return Task.Run(() => 
            {
                while(!cancellationToken.IsCancellationRequested)
                {
                    var environmentDataModel = influxDbRepository.GetLastEnvironmentState();
                    if(environmentDataModel != default)
                    {
                        CheckAndNotifyCo2(environmentDataModel);
                        CheckAndNotifyLight(environmentDataModel);
                        CheckAndNotifyTemperature(environmentDataModel);
                        CheckAndNotifyNoise(environmentDataModel);
                        CheckAndNotifyHumidity(environmentDataModel);
                    }

                    Thread.Sleep(configuration.GetValue<int>("AlertersDelayMs"));
                }
            });
        }

        private void CheckAndNotifyCo2(EnvironmentDataModel environmentDataModel)
        {
            if(environmentDataModel.Co2.HasValue)
            {
                if(co2AlertHelper.IsOutOfHighAlertRange(environmentDataModel.Co2.Value))
                    notifier.Notify(environmentDataModel.Co2.Value, "CO2", AlertType.High);
                else if(co2AlertHelper.IsOutOfMediumAlertRange(environmentDataModel.Co2.Value))
                    notifier.Notify(environmentDataModel.Co2.Value, "CO2", AlertType.Medium);
            }
        }
        private void CheckAndNotifyLight(EnvironmentDataModel environmentDataModel)
        {
            if(environmentDataModel.Light.HasValue)
            {
                if(lightAlertHelper.IsOutOfHighAlertRange(environmentDataModel.Light.Value))
                    notifier.Notify(environmentDataModel.Light.Value, "Light", AlertType.High);
                else if(lightAlertHelper.IsOutOfMediumAlertRange(environmentDataModel.Light.Value))
                    notifier.Notify(environmentDataModel.Light.Value, "Light", AlertType.Medium);
            }
        }
        private void CheckAndNotifyNoise(EnvironmentDataModel environmentDataModel)
        {
            if(environmentDataModel.Noise.HasValue)
            {
                if(noiseAlertHelper.IsOutOfHighAlertRange(environmentDataModel.Noise.Value))
                    notifier.Notify(environmentDataModel.Noise.Value, "Noise", AlertType.High);
                else if(noiseAlertHelper.IsOutOfMediumAlertRange(environmentDataModel.Noise.Value))
                    notifier.Notify(environmentDataModel.Noise.Value, "Noise", AlertType.Medium);
            }
        }
        private void CheckAndNotifyTemperature(EnvironmentDataModel environmentDataModel)
        {
            if(environmentDataModel.Temperature.HasValue)
            {
                if(temperatureAlertHelper.IsOutOfHighAlertRange(environmentDataModel.Temperature.Value))
                    notifier.Notify(environmentDataModel.Temperature.Value, "Temperature", AlertType.High);
                else if(temperatureAlertHelper.IsOutOfMediumAlertRange(environmentDataModel.Temperature.Value))
                    notifier.Notify(environmentDataModel.Temperature.Value, "Temperature", AlertType.Medium);
            }
        }
        private void CheckAndNotifyHumidity(EnvironmentDataModel environmentDataModel)
        {
            if(environmentDataModel.Humidity.HasValue)
            {
                if(humidityAlertHelper.IsOutOfHighAlertRange(environmentDataModel.Humidity.Value))
                    notifier.Notify(environmentDataModel.Humidity.Value, "Humidity", AlertType.High);
                else if(humidityAlertHelper.IsOutOfMediumAlertRange(environmentDataModel.Humidity.Value))
                    notifier.Notify(environmentDataModel.Humidity.Value, "Humidity", AlertType.Medium);
            }
        }
    }
}