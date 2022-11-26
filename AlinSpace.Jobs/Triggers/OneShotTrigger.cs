namespace AlinSpace.Jobs
{
    public class OneShotTrigger : ITrigger
    {
        private readonly DateTimeOffset timestamp = DateTimeOffset.UtcNow;
        private readonly TimeSpan dueTime;

        public OneShotTrigger(TimeSpan? dueTime = null)
        {
            this.dueTime = dueTime ?? TimeSpan.Zero;
        }

        public TimeSpan? GetDueTime(IJobInfo jobInfo)
        {
            if (jobInfo.NumberOfExecutions > 0)
                return null;

            return timestamp.Add(dueTime) - DateTimeOffset.UtcNow;
        }
    }
}
