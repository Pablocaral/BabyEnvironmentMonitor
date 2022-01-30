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
        public HistoryModule(IHistoryService service) : base("/")
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

                var result = service.GetCo2History(Context, parameters.environmentUid);
                if(result == null)
                {
                    return new Response()
                    {
                        ReasonPhrase = "Internal server error",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                return result;
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

                var result = service.GetHumidityHistory(Context, parameters.environmentUid);
                if(result == null)
                {
                    return new Response()
                    {
                        ReasonPhrase = "Internal server error",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                return result;
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

                var result = service.GetLightHistory(Context, parameters.environmentUid);
                if(result == null)
                {
                    return new Response()
                    {
                        ReasonPhrase = "Internal server error",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                return result;
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

                var result = service.GetNoiseHistory(Context, parameters.environmentUid);
                if(result == null)
                {
                    return new Response()
                    {
                        ReasonPhrase = "Internal server error",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                return result;
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

                var result = service.GetTemperatureHistory(Context, parameters.environmentUid);
                if(result == null)
                {
                    return new Response()
                    {
                        ReasonPhrase = "Internal server error",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                return result;
            });
        }
    }

    /// <summary>
    /// Service handling History requests.
    /// </summary>
    public interface IHistoryService
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