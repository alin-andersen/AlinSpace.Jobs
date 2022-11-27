namespace AlinSpace.Jobs
{
    internal class JobExecutionsTracker : IDisposable
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

        public void JobExecutionStarted()
        {
            ots.ThrowObjectDisposedIfSet<JobExecutionsTracker>();

            @lock.LockDelegate(() =>
            {
                numberOfExecutions += 1;
                manualResetEventSlim.Reset();
            });
        }

        public void JobExecutionStopped()
        {
            ots.ThrowObjectDisposedIfSet<JobExecutionsTracker>();

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

        public void WaitForJobsToFinishExecution()
        {
            ots.ThrowObjectDisposedIfSet<JobExecutionsTracker>();

            manualResetEventSlim.Wait();
        }
    }
}
