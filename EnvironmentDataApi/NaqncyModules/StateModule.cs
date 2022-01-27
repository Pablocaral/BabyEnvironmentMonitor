using Com.EnvironmentDataApi.NancyModels;
using Nancy;

namespace Com.EnvironmentDataApi.NancyModules
{ 

    /// <summary>
    /// Module processing requests of State domain.
    /// </summary>
    public sealed class StateModule : NancyModule
    {
        /// <summary>
        /// Sets up HTTP methods mappings.
        /// </summary>
        /// <param name="service">Service handling requests</param>
        public StateModule(IStateService service) : base("/")
        { 
            Get("/state/current/{environmentUid}", parameters =>
            {
                if(string.IsNullOrEmpty(parameters.environmentUid))
                    return new Response()
                    {
                        ReasonPhrase = "Required parameter: 'environmentUid' is missing at 'GetCurrentState'",
                        StatusCode = HttpStatusCode.BadRequest
                    };

                return service.GetCurrentState(Context, parameters.environmentUid);
            });
        }
    }

    /// <summary>
    /// Service handling State requests.
    /// </summary>
    public interface IStateService
    {
        /// <summary>
        /// Get current state of a baby environment, given its unique identifier
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="environmentUid">The baby environment unique identifier</param>
        /// <returns>EnvironmentState</returns>
        EnvironmentState GetCurrentState(NancyContext context, string environmentUid);
    }
}