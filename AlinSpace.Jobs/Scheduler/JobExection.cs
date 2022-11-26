namespace AlinSpace.Jobs
{
    internal class JobExection : IJobExecution
    {
        public DateTimeOffset Started { get; set; }

        public DateTimeOffset Stopped { get; set; }

        public TimeSpan ExecutionDuration { get; set; }

        public bool DidThrowException => ThrownException != null;

        public Exception? ThrownException { get; set; }
    }
}