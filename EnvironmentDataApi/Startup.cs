using Com.EnvironmentDataApi.NancyModules;
using Com.EnvironmentDataApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace Com.EnvironmentDataApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IStateService), typeof(StateService)); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOwin(x => x.UseNancy());
        }
    }
}