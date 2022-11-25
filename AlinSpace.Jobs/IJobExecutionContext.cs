namespace AlinSpace.Jobs
{
    public interface IJobExecutionContext
    {
        IEnumerable<object> Parameters { get; }

        CancellationToken CancellationToken { get; }

        object Result { get; set; }
    }
}