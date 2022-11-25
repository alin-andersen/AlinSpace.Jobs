using AlinSpace.Jobs.SingleThreadJobExecutor;

namespace AlinSpace.Jobs.SingleThreadScheduler
{
    public class SingleThreadJobExecutor : IJobExecutor
    {
        private CancellationTokenSource tcs;
        private Thread thread;
        private bool isRunning = false;

        private Queue<SingleThreadJob> queue = new Queue<SingleThreadJob>();
        private SpinLock queueLock = new();

        public void Start()
        {
            tcs = new CancellationTokenSource();
            thread = new Thread(OnThread);
            thread.Start();
        }

        public void Stop(bool waitForJobsToFinish = true)
        {
            tcs.
        }

        public Task RunAsync(IJob job, IJobExecutionContext context)
        {

        }


        private async void OnThread(object? obj)
        {
            while(true)
            {
                var singleThreadJob = queueLock.LockDelegate(queue.Dequeue);

                try
                {
                    singleThreadJob.Job.ExecuteAsync(singleThreadJob.JobExecutionContext).ConfigureAwait(true).GetAwaiter().GetResult();
                }
                catch(Exception e)
                {

                }
            }
        }
    }
}
