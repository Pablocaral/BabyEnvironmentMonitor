using Com.EnvironmentDataApi.NancyModules;
using Com.EnvironmentDataApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace Com.EnvironmentDataApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var stateService = new StateService();
            stateService.configuration = configuration;
            services.AddSingleton<IStateService>(stateService);

            var historyService = new HistoryService();
            historyService.configuration = configuration;
            services.AddSingleton<IHistoryService>(historyService);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOwin(x => x.UseNancy());
        }
    }
}