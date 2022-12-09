namespace AlinSpace.Jobs.Triggers
{
    /// <summary>
    /// Represents the recurring trigger.
    /// </summary>
    public class RecurringTrigger : ITrigger
    {
        private readonly TimeSpan interval;
        private readonly int? times;
        private readonly TimeSpan? delay;
        private readonly DateTimeOffset firstExecutionTimestamp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interval">Interval.</param>
        /// <param name="times">Times.</param>
        /// <param name="delay">Delay.</param>
        public RecurringTrigger(TimeSpan interval, int? times = null, TimeSpan? delay = null)
        {
            this.interval = interval.Duration();
            this.times = times;
            this.delay = delay?.Duration();

            firstExecutionTimestamp = DateTimeOffset.UtcNow.Add(this.delay ?? TimeSpan.Zero);
        }

        public TimeSpan? GetDueTime(IJobInfo jobInfo)
        {
            // If we have a limit, then return if we have reached it.
            if (times.HasValue && times.Value > 0)
            {
                if (jobInfo.NumberOfExecutions >= times.Value)
                    return null;
            }

            var nextExecutionTimestamp = firstExecutionTimestamp.Add(interval * jobInfo.NumberOfExecutions);

            return nextExecutionTimestamp - DateTimeOffset.UtcNow;
        }
    }
}
