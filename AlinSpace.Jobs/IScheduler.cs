namespace AlinSpace.Jobs
{
    public interface IScheduler : IDisposable
    {
        bool IsRunning { get; }

        void Start();

        void Stop(bool waitForJobsToFinish = true);

        #region Schedule

        IEnumerable<IJobInfo> Jobs { get; }

        long ScheduleJob(IJob job, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null);
        
        long ScheduleJob(Type jobType, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null);
        
        void RemoveJob(long id);

        void RemoveAllJobs();

        void UnpauseJob(long id);

        void PauseJob(long id);

        #endregion
    }
}
