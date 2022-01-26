using Nancy;

namespace Com.EnvironmentDataApi.Modules
{
    public sealed class SwaggerModule : NancyModule
    {
        public SwaggerModule()
        {
            Get("swagger", args =>
            {
                return Response. AsRedirect ( "/swagger/index.html" );     
            });
        }
    }
}