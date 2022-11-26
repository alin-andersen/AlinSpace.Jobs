namespace AlinSpace.Jobs
{
    public class JobExecutionContext : IJobExecutionContext
    {
        public IJobInfo Info { get; }

        public CancellationToken CancellationToken { get; }

        public JobExecutionContext(IJobInfo info, CancellationToken cancellationToken)
        {
            Info = info;
            CancellationToken = cancellationToken;
        }
    }
}