namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job interface.
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// Executes asynchronously.
        /// </summary>
        /// <param name="context">Job execution context.</param>
        Task ExecuteAsync(IJobExecutionContext context);
    }
}