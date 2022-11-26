namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job info.
    /// </summary>
    public interface IJobInfo
    {
        /// <summary>
        /// Gets the job ID.
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Gets the job key.
        /// </summary>
        object? Key { get; }

        /// <summary>
        /// Gets the job state.
        /// </summary>
        JobState State { get; }

        /// <summary>
        /// Gets the number of executions.
        /// </summary>
        int NumberOfExecutions { get; }
    }
}
