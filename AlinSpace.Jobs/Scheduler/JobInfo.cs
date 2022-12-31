namespace AlinSpace.Jobs
{
    internal class JobInfo : IJobInfo
    {
        #region Read Only

        public long Id { get; set; }

        public object? Key { get; }

        public DateTime CreationTimestamp { get; } = DateTime.UtcNow;

        public Type? JobType { get; }

        public IJob? Job { get; }

        public ITrigger Trigger { get; }

        #endregion

        #region Locked

        public JobState State { get; set; }

        public bool IsRemoved { get; set; }

        public int NumberOfExecutions { get; set; }

        #endregion

        public JobInfo(IJob job, ITrigger trigger, object? key)
        {
            Job = job;
            Trigger = trigger;
            Key = key;
        }

        public JobInfo(Type? jobType, ITrigger trigger, object? key)
        {
            Key = key;
            Trigger = trigger;
            JobType = jobType;
        }
    }
}
