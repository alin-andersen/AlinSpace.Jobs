namespace AlinSpace.Jobs.Triggers
{
    public class RecurringTrigger : ITrigger
    {
        private readonly DateTimeOffset timestamp = DateTimeOffset.UtcNow;
        private readonly TimeSpan interval;
        private readonly int? times;
        private readonly TimeSpan? delay;

        public RecurringTrigger(TimeSpan interval, int? times = null, TimeSpan? delay = null)
        {
            this.interval = interval;
            this.times = times;
            this.delay = delay;
        }

        public TimeSpan? GetDueTime(IJobInfo jobInfo)
        {
            if (times.HasValue && times.Value > 0)
            {
                if (jobInfo.NumberOfExecutions >= times.Value)
                    return null;
            }

            var firstExecutionTimestamp = timestamp.Add(delay ?? TimeSpan.Zero);

            var currentTimestamp = DateTimeOffset.UtcNow;

            if (jobInfo.NumberOfExecutions == 0)
            {
                return firstExecutionTimestamp - currentTimestamp;
            }
            else
            {
                var nextExecutionTimestamp = firstExecutionTimestamp.Add(interval * jobInfo.NumberOfExecutions);

                return nextExecutionTimestamp - currentTimestamp;
            }
        }
    }
}
