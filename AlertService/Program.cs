using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using Com.AlertService.Alerters;
using log4net;
using Microsoft.Extensions.Configuration;
using Ninject;

namespace Com.AlertService
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public static void Main(string[] args)
        {
            try
            {
                var configuration = LoadConfigutation(args);
                InitLog();
                var kernel = new StandardKernel(new NinjectImpl(configuration));

                var exitEvent = new ManualResetEvent(false);
                Console.CancelKeyPress += (sender, args) =>
                {
                    exitEvent.Set();
                    args.Cancel = true;
                };

                var cancellationTokenSorce = new CancellationTokenSource();
                var alerter = kernel.Get<Alerter>();
                var alerterTask = alerter.Run(cancellationTokenSorce.Token);

                log.Info("Alert service is started.");

                exitEvent.WaitOne();
                cancellationTokenSorce.Cancel();
                alerterTask.Wait();
                
                log.Info("Alert service has been finish.");
            }
            catch(Exception ex)
            {
                log.Error("An error executing Alert Service", ex);
            }
        }
        private static IConfiguration LoadConfigutation(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("config.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddEnvironmentVariables();
            configurationBuilder.AddCommandLine(args);
            return configurationBuilder.Build();
        }
        private static void InitLog()
        {
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
    }
}