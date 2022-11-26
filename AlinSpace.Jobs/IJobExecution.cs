namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents teh job execution.
    /// </summary>
    public interface IJobExecution
    {
        /// <summary>
        /// Gets the started timestamp.
        /// </summary>
        DateTimeOffset Started { get; }

        /// <summary>
        /// Gets the stopped timestamp.
        /// </summary>
        DateTimeOffset Stopped { get; }

        /// <summary>
        /// Gets the duration of the execution.
        /// </summary>
        TimeSpan ExecutionDuration { get; }

        /// <summary>
        /// Gets the flag indicating wether or not the execution did throw an exception.
        /// </summary>
        bool DidThrowException { get; }

        /// <summary>
        /// Gets the thrown exception.
        /// </summary>
        Exception? ThrownException { get; }
    }
}
