namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the scheduler interface.
    /// </summary>
    public interface IScheduler : IDisposable
    {
        /// <summary>
        /// Gets the flag indicating wether or not the scheduler is running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Starts the scheduler.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the scheduler.
        /// </summary>
        /// <param name="waitForJobsToFinish">Wait for jobs to finish.</param>
        void Stop(bool waitForJobsToFinish = true);

        /// <summary>
        /// Stops the scheduler asynchronously.
        /// </summary>
        /// <param name="waitForJobsToFinish">Wait for jobs to finish.</param>
        Task StopAsync(bool waitForJobsToFinish = true);

        #region Schedule

        /// <summary>
        /// Gets the jobs.
        /// </summary>
        IEnumerable<IJobInfo> Jobs { get; }

        /// <summary>
        /// Schedules the job.
        /// </summary>
        /// <param name="job">Job instance.</param>
        /// <param name="trigger">Trigger.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="key">Key.</param>
        /// <returns>Job ID.</returns>
        long ScheduleJob(IJob job, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null);
        
        /// <summary>
        /// Schedules the job.
        /// </summary>
        /// <param name="jobType">Job type.</param>
        /// <param name="trigger">Trigger.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="key">Key.</param>
        /// <returns>Job ID.</returns>
        long ScheduleJob(Type jobType, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null);
        
        /// <summary>
        /// Removes the job.
        /// </summary>
        /// <param name="id">Job ID.</param>
        void RemoveJob(long id);

        /// <summary>
        /// Removes all jobs.
        /// </summary>
        void RemoveAllJobs();

        #endregion
    }
}
