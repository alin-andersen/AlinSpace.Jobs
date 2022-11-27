namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job execution tracker.
    /// </summary>
    internal class JobExecutionTracker : IDisposable
    {
        private readonly OneTimeSwitch ots = new();
        private readonly ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim();
        private SpinLock @lock = new();
        private int numberOfExecutions = 0;

        public void Dispose()
        {
            if (!ots.TrySet())
                return;

            manualResetEventSlim.Dispose();
        }

        /// <summary>
        /// Called before job execution.
        /// </summary>
        public void JobExecutionStarted()
        {
            ots.ThrowObjectDisposedIfSet<JobExecutionTracker>();

            @lock.LockDelegate(() =>
            {
                numberOfExecutions += 1;
                manualResetEventSlim.Reset();
            });
        }

        /// <summary>
        /// Called after job execution.
        /// </summary>
        public void JobExecutionStopped()
        {
            ots.ThrowObjectDisposedIfSet<JobExecutionTracker>();

            @lock.LockDelegate(() =>
            {
                if (numberOfExecutions > 0)
                {
                    numberOfExecutions -= 1;
                }

                if (numberOfExecutions == 0)
                {
                    manualResetEventSlim.Set();
                }
            });
        }

        /// <summary>
        /// Wait for job executions to finish.
        /// </summary>
        public void WaitForJobsToFinishExecution()
        {
            ots.ThrowObjectDisposedIfSet<JobExecutionTracker>();

            manualResetEventSlim.Wait();
        }
    }
}
