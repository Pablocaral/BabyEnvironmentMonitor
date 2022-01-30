using System.IO;
using System.Reflection;
using System.Xml;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Com.EnvironmentDataApi
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            InitLog();
            CreateHostBuilder(args).Build().Run();
            log.Info("Environment Data API is started.");
        }

        private static void InitLog()
        {
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                        .UseKestrel(o => o.AllowSynchronousIO = true)
                        .UseStartup<Startup>()
                        .UseConfiguration(LoadConfigutation(args));
                });
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