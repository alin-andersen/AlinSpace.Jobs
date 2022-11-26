namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job execution context.
    /// </summary>
    public interface IJobExecutionContext
    {
        /// <summary>
        /// Gets the job info.
        /// </summary>
        IJobInfo Info { get; }

        /// <summary>
        /// Gets the cancellation token.
        /// </summary>
        CancellationToken CancellationToken { get; }
    }
}