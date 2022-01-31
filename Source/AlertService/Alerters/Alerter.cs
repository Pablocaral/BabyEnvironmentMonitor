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
                    CheckAndNotifyCo2();
                    CheckAndNotifyLight();
                    CheckAndNotifyTemperature();
                    CheckAndNotifyNoise();
                    CheckAndNotifyHumidity();

                    Thread.Sleep(configuration.GetValue<int>("AlertersDelayMs"));
                }
            });
        }

        private void CheckAndNotifyCo2()
        {
            var lastCo2 = influxDbRepository.GetLastSensorValue("co2");
            if(lastCo2.HasValue)
            {
                if(co2AlertHelper.IsOutOfHighAlertRange(lastCo2.Value))
                    notifier.Notify(lastCo2.Value, "CO2", AlertType.High);
                else if(co2AlertHelper.IsOutOfMediumAlertRange(lastCo2.Value))
                    notifier.Notify(lastCo2.Value, "CO2", AlertType.Medium);
            }
        }
        private void CheckAndNotifyLight()
        {
            var lastLight = influxDbRepository.GetLastSensorValue("light");
            if(lastLight.HasValue)
            {
                if(lightAlertHelper.IsOutOfHighAlertRange(lastLight.Value))
                    notifier.Notify(lastLight.Value, "Light", AlertType.High);
                else if(lightAlertHelper.IsOutOfMediumAlertRange(lastLight.Value))
                    notifier.Notify(lastLight.Value, "Light", AlertType.Medium);
            }
        }
        private void CheckAndNotifyNoise()
        {
            var lastNoise = influxDbRepository.GetLastSensorValue("noise");
            if(lastNoise.HasValue)
            {
                if(noiseAlertHelper.IsOutOfHighAlertRange(lastNoise.Value))
                    notifier.Notify(lastNoise.Value, "Noise", AlertType.High);
                else if(noiseAlertHelper.IsOutOfMediumAlertRange(lastNoise.Value))
                    notifier.Notify(lastNoise.Value, "Noise", AlertType.Medium);
            }
        }
        private void CheckAndNotifyTemperature()
        {
            var lastTemperature = influxDbRepository.GetLastSensorValue("temperature");
            if(lastTemperature.HasValue)
            {
                if(temperatureAlertHelper.IsOutOfHighAlertRange(lastTemperature.Value))
                    notifier.Notify(lastTemperature.Value, "Temperature", AlertType.High);
                else if(temperatureAlertHelper.IsOutOfMediumAlertRange(lastTemperature.Value))
                    notifier.Notify(lastTemperature.Value, "Temperature", AlertType.Medium);
            }
        }
        private void CheckAndNotifyHumidity()
        {
            var lastHumidity = influxDbRepository.GetLastSensorValue("humidity");
            if(lastHumidity.HasValue)
            {
                if(humidityAlertHelper.IsOutOfHighAlertRange(lastHumidity.Value))
                    notifier.Notify(lastHumidity.Value, "Humidity", AlertType.High);
                else if(humidityAlertHelper.IsOutOfMediumAlertRange(lastHumidity.Value))
                    notifier.Notify(lastHumidity.Value, "Humidity", AlertType.Medium);
            }
        }
    }
}