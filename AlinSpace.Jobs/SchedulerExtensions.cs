namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the extensions for <see cref="IScheduler"/>.
    /// </summary>
    public static class SchedulerExtensions
    {
        /// <summary>
        /// Schedules the job.
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="scheduler">Scheduler.</param>
        /// <param name="trigger">Trigger.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="key">Key.</param>
        /// <returns>Job ID.</returns>
        public static long ScheduleJob<TJob>(this IScheduler scheduler, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null) where TJob : IJob
        {
            return scheduler.ScheduleJob(typeof(TJob), trigger, parameters, key);
        }
    }
}
