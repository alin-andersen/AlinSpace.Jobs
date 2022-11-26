namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job factory.
    /// </summary>
    public interface IJobFactory
    {
        /// <summary>
        /// Creates the job.
        /// </summary>
        /// <param name="jobType">Job type.</param>
        /// <returns>Job.</returns>
        IJob CreateJob(Type jobType);
    }
}
