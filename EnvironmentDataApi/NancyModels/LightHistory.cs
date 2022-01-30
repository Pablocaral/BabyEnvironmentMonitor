using System.Collections.Generic;

namespace Com.EnvironmentDataApi.NancyModels
{
    /// <summary>
    /// The history, compose by the last 7 days, of the light detected on the baby environment. 
    /// Time period could be less than this one if no previous data has registered on the system.
    /// </summary>
    public sealed class LightHistory
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
        public List<float?> HistoryData { get; private set; }

        public LightHistory()
        {
        }
        public LightHistory(Period Period, int? TimeInterval, List<float?> HistoryData)
        {
            this.Period = Period;   
            this.TimeInterval = TimeInterval;
            this.HistoryData = HistoryData;
        }       
    }
}