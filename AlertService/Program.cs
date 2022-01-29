using System;
using System.Threading;
using Com.AlertService.Alerters;
using Microsoft.Extensions.Configuration;
using Ninject;

namespace Com.AlertService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = LoadConfigutation(args);
            var kernel = new StandardKernel(new NinjectImpl(configuration));

            var exitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, args) =>
            {
                exitEvent.Set();
                args.Cancel = true;
            };

            var cancellationTokenSorce = new CancellationTokenSource();
            var co2Alerter = kernel.Get<IAlerter>();
            var co2AlerterTask = co2Alerter.Run(cancellationTokenSorce.Token);

            exitEvent.WaitOne();
            cancellationTokenSorce.Cancel();
            co2AlerterTask.Wait();
        }
        private static IConfiguration LoadConfigutation(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("config.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddEnvironmentVariables();
            configurationBuilder.AddCommandLine(args);
            return configurationBuilder.Build();
        }
    }
}