namespace AlinSpace.Jobs
{
    public interface IJobExecutor
    {
        void Start();

        void Stop(bool waitForJobsToFinish = true);

        Task RunAsync(IJob job, IJobExecutionContext context);
    }
}
