namespace AlinSpace.Jobs
{
    public class JobExecutionContext : IJobExecutionContext
    {
        public IJobInfo Info { get; }

        public IEnumerable<object> Parameters { get; }

        public CancellationToken CancellationToken { get; }

        public object Result { get; set; }

        public JobExecutionContext(IJobInfo info, IEnumerable<object>? parameters, CancellationToken cancellationToken)
        {
            Info = info;
            Parameters = parameters ?? Enumerable.Empty<object>();
            CancellationToken = cancellationToken;
        }
    }
}