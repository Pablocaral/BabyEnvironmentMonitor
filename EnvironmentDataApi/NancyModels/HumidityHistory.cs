using System.Collections.Generic;

namespace Com.EnvironmentDataApi.NancyModels
{
    /// <summary>
    /// The history, compose by the last 7 days, of the humidity detected on the baby environment. 
    /// Time period could be less than this one if no previous data has registered on the system.
    /// </summary>
    public sealed class HumidityHistory
    { 
        /// <summary>
        /// Period
        /// </summary>
        public Period Period { get; private set; }

        /// <summary>
        /// Elapsed time interval between each history item
        /// </summary>
        public int? TimeInterval { get; private set; }

        /// <summary>
        /// HistoryData
        /// </summary>
        public List<decimal?> HistoryData { get; private set; }

        public HumidityHistory()
        {
        }
        private HumidityHistory(Period Period, int? TimeInterval, List<decimal?> HistoryData)
        {
            this.Period = Period;   
            this.TimeInterval = TimeInterval;
            this.HistoryData = HistoryData;
        }       
    }
}