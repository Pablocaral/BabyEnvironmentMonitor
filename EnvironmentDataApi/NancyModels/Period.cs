using System;

namespace Com.EnvironmentDataApi.NancyModels
{
    /// <summary>
    /// Period
    /// </summary>
    public sealed class Period
    { 
        /// <summary>
        /// Date and time when the period start
        /// </summary>
        public DateTimeOffset? StartPeriod { get; private set; }

        /// <summary>
        /// Date and time when the period finish
        /// </summary>
        public DateTimeOffset? EndPeriod { get; private set; }

        public Period()
        {
        }
        public Period(DateTimeOffset? StartPeriod, DateTimeOffset? EndPeriod)
        {
            this.StartPeriod = StartPeriod;   
            this.EndPeriod = EndPeriod;
        }
    }
}