namespace AlinSpace.Jobs
{
    public interface IJobInfo
    {
        long Id { get; }

        object? Key { get; }

        JobState State { get; }

        int NumberOfExecutions { get; }

        IEnumerable<IJobExecution> Executions { get; }
    }
}
