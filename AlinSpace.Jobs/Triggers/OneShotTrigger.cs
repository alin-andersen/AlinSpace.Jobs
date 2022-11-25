namespace AlinSpace.Jobs
{
    public class OneShotTrigger : ITrigger
    {
        public TimeSpan DueTime { get; }

        public OneShotTrigger(TimeSpan? dueTime = null)
        {
            DueTime = dueTime ?? TimeSpan.Zero;
        }

        public TimeSpan GetDueTime(IJobInfo jobInfo)
        {
            if (jobInfo.NumberOfExecutions > 0)
                return Timeout.InfiniteTimeSpan;

            return DueTime;
        }
    }
}
