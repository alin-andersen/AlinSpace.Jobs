namespace AlinSpace.Jobs
{
    public interface IJobExecution
    {
        DateTimeOffset StartTimestamp { get; }

        DateTimeOffset StopTimestamp { get; }

        TimeSpan Duration { get; }

        bool DidThrowException { get; }

        Exception ThrownException { get; }

        IEnumerable<object> Parameters { get; }

        object Result { get; set; }
    }
}
