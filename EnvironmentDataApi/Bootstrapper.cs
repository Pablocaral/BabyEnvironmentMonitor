using Nancy;
using Nancy.Conventions;

namespace Com.EnvironmentDataApi
{
    public class Bootstrapper : DefaultNancyBootstrapper  
    {  
        protected override void ConfigureConventions(NancyConventions nancyConventions)  
        {  
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("swagger"));  
        }  
    } 
}