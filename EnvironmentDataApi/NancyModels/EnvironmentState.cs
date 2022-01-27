namespace Com.EnvironmentDataApi.NancyModels
{
    /// <summary>
    /// The state of the baby environment, composed by the current last value obtained by each sensor.
    /// </summary>
    public sealed class EnvironmentState
    { 
        /// <summary>
        /// CO2
        /// </summary>
        public decimal? CO2 { get; set; }

        /// <summary>
        /// Temperature
        /// </summary>
        public decimal? Temperature { get; set; }

        /// <summary>
        /// Light
        /// </summary>
        public decimal? Light { get; set; }

        /// <summary>
        /// Noise
        /// </summary>
        public decimal? Noise { get; set; }

        /// <summary>
        /// Humidity
        /// </summary>
        public decimal? Humidity { get; set; }
        
        public EnvironmentState()
        {
        }

        private EnvironmentState(decimal? CO2, decimal? Temperature, decimal? Light, decimal? Noise, decimal? Humidity)
        {
            
            this.CO2 = CO2;
            
            this.Temperature = Temperature;
            
            this.Light = Light;
            
            this.Noise = Noise;
            
            this.Humidity = Humidity;
            
        }
    }
}