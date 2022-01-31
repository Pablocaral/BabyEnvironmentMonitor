using Com.AlertService.Alerters;
using Com.AlertService.Alerters.Helpers;
using Com.AlertService.Notifiers;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;

namespace Com.AlertService
{
    public class NinjectImpl : NinjectModule
    {
        private IConfiguration configuration;

        public NinjectImpl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override void Load()
        {
            Bind<IConfiguration>().ToConstant(configuration);
            Bind<Alerter>().To<Alerter>().InSingletonScope();
            Bind<INotifier>().To<ConsoleNotifier>().InSingletonScope();

            Bind<Co2AlertHelper>().To<Co2AlertHelper>().InSingletonScope();
            Bind<HumididtyAlertHelper>().To<HumididtyAlertHelper>().InSingletonScope();
            Bind<LightAlertHelper>().To<LightAlertHelper>().InSingletonScope();
            Bind<NoiseAlertHelper>().To<NoiseAlertHelper>().InSingletonScope();
            Bind<TemperatureAlertHelper>().To<TemperatureAlertHelper>().InSingletonScope();
        }
    }
}