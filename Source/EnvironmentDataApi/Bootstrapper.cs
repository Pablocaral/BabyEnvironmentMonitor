using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace Com.EnvironmentDataApi
{
    public class Bootstrapper : DefaultNancyBootstrapper  
    {
        private IConfiguration config;

        public Bootstrapper(IConfiguration config)
        {
            this.config = config;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)  
        {  
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("swagger"));  
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<IConfiguration>(config);
        }
    } 
}