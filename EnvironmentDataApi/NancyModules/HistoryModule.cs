using Com.EnvironmentDataApi.NancyModels;
using Nancy;

namespace Com.EnvironmentDataApi.NancyModules
{ 
    /// <summary>
    /// Module processing requests of History domain.
    /// </summary>
    public sealed class HistoryModule : NancyModule
    {
        /// <summary>
        /// Sets up HTTP methods mappings.
        /// </summary>
        /// <param name="service">Service handling requests</param>
        public HistoryModule(HistoryService service) : base("/")
        { 
            Get("/history/co2/{environmentUid}", parameters =>
            {
                if(string.IsNullOrEmpty(parameters.environmentUid))
                {
                    return new Response()
                    {
                        ReasonPhrase = "Required parameter: 'environmentUid' is missing at 'GetCurrentState'",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                return service.GetCo2History(Context, parameters.environmentUid);
            });

            Get("/history/humidity/{environmentUid}", parameters =>
            {
                if(string.IsNullOrEmpty(parameters.environmentUid))
                {
                    return new Response()
                    {
                        ReasonPhrase = "Required parameter: 'environmentUid' is missing at 'GetCurrentState'",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                return service.GetHumidityHistory(Context, parameters.environmentUid);
            });

            Get("/history/light/{environmentUid}", parameters =>
            {
                if(string.IsNullOrEmpty(parameters.environmentUid))
                {
                    return new Response()
                    {
                        ReasonPhrase = "Required parameter: 'environmentUid' is missing at 'GetCurrentState'",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                return service.GetLightHistory(Context, parameters.environmentUid);
            });

            Get("/history/noise/{environmentUid}", parameters =>
            {
                if(string.IsNullOrEmpty(parameters.environmentUid))
                {
                    return new Response()
                    {
                        ReasonPhrase = "Required parameter: 'environmentUid' is missing at 'GetCurrentState'",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                return service.GetNoiseHistory(Context, parameters.environmentUid);
            });

            Get("/history/temperature/{environmentUid}", parameters =>
            {
                if(string.IsNullOrEmpty(parameters.environmentUid))
                {
                    return new Response()
                    {
                        ReasonPhrase = "Required parameter: 'environmentUid' is missing at 'GetCurrentState'",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                return service.GetTemperatureHistory(Context, parameters.environmentUid);
            });
        }
    }

    /// <summary>
    /// Service handling History requests.
    /// </summary>
    public interface HistoryService
    {
        /// <summary>
        /// Get the CO2 history data of a baby environment, given its unique identifier
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="environmentUid">The baby environment unique identifier</param>
        /// <returns>Co2History</returns>
        Co2History GetCo2History(NancyContext context, string environmentUid);

        /// <summary>
        /// Get the humidity history data of a baby environment, given its unique identifier
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="environmentUid">The baby environment unique identifier</param>
        /// <returns>HumidityHistory</returns>
        HumidityHistory GetHumidityHistory(NancyContext context, string environmentUid);

        /// <summary>
        /// Get the light history data of a baby environment, given its unique identifier
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="environmentUid">The baby environment unique identifier</param>
        /// <returns>LightHistory</returns>
        LightHistory GetLightHistory(NancyContext context, string environmentUid);

        /// <summary>
        /// Get the noise history data of a baby environment, given its unique identifier
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="environmentUid">The baby environment unique identifier</param>
        /// <returns>NoiseHistory</returns>
        NoiseHistory GetNoiseHistory(NancyContext context, string environmentUid);

        /// <summary>
        /// Get the temperature history data of a baby environment, given its unique identifier
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="environmentUid">The baby environment unique identifier</param>
        /// <returns>TemperatureHistory</returns>
        TemperatureHistory GetTemperatureHistory(NancyContext context, string environmentUid);
    }
}