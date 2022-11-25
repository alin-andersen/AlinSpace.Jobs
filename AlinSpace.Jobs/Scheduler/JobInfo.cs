using System.Collections.ObjectModel;

namespace AlinSpace.Jobs
{
    internal class JobInfo : IJobInfo
    {
        #region Read Only

        public long Id { get; set; }

        public object? Key { get; }

        public Type? JobType { get; }

        public IJob? Job { get; set; }

        public ITrigger Trigger { get; }

        #endregion

        public JobState State { get; set; }

        public int NumberOfExecutions { get; private set; }

        public IEnumerable<IJobExecution> Executions => new ReadOnlyCollection<IJobExecution>(executions);

        private readonly IList<IJobExecution> executions = new List<IJobExecution>();

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

        public void AddJobExecution(IJobExecution jobExecution)
        {
            NumberOfExecutions += 1;
            executions.Add(jobExecution);
        }
    }
}
