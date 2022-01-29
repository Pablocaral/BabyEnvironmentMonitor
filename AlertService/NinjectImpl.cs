using Com.AlertService.Alerters;
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
            Bind<IAlerter>().To<Co2Alerter>().InSingletonScope();
            Bind<INotifier>().To<ConsoleNotifier>().InSingletonScope();
        }
    }
}