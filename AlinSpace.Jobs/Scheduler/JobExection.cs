namespace AlinSpace.Jobs
{
    internal class JobExection : IJobExecution
    {
        public DateTimeOffset StartTimestamp { get; set; }

        public DateTimeOffset StopTimestamp { get; set; }

        public TimeSpan Duration { get; set; }

        public bool DidThrowException => ThrownException != null;

        public Exception ThrownException { get; set; }

        public IEnumerable<object> Parameters { get; set; }

        public object Result { get; set; }
    }
}