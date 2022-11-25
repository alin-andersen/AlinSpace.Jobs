namespace AlinSpace.Jobs
{
    internal class DefaultJobExecutor : IJobExecutor
    {
        public void Start()
        {
        }

        public void Stop(bool waitForJobsToFinish = true)
        {
        }

        public async Task RunAsync(IJob job, IJobExecutionContext context)
        {
            await job.ExecuteAsync(context);
        }
    }
}
