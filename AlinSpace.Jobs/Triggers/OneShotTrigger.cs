namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the one shot trigger.
    /// </summary>
    public class OneShotTrigger : ITrigger
    {
        private readonly DateTimeOffset timestamp = DateTimeOffset.UtcNow;
        private readonly TimeSpan dueTime;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dueTime">Due time.</param>
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
