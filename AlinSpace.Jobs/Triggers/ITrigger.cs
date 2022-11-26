namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the trigger.
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// Gets the due time.
        /// </summary>
        /// <param name="jobInfo">Job info.</param>
        /// <returns>Due time or null.</returns>
        /// <remarks>
        /// Null is returend to signal that the trigger is done.
        /// Meaning it will never ever trigger a job execution.
        /// </remarks>
        TimeSpan? GetDueTime(IJobInfo jobInfo);
    }
}
