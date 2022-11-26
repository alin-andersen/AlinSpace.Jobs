namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the scheduler.
    /// </summary>
    public class Scheduler : IScheduler
    {
        private readonly IJobFactory jobFactory;
        private readonly JobRegistry jobRegistry;
        private readonly SchedulerTimer timer;
        private readonly OneTimeSwitch ots = new();

        private SpinLock @lock = new();

        public IEnumerable<IJobInfo> Jobs => jobRegistry.Jobs;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="jobFactory">Job factory.</param>
        public Scheduler(IJobFactory? jobFactory = null)
        {
            this.jobFactory = jobFactory ?? new DefaultJobFactory();
            jobRegistry = new JobRegistry();
            timer = new SchedulerTimer(jobRegistry, OnTimerTriggered);
        }

        public bool IsRunning => isRunning;
        private bool isRunning = false;

        public void Start()
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            var didStart = @lock.LockDelegate(() =>
            {
                if (isRunning)
                    return false;

                isRunning = true;
                return true;
            });

            if (didStart)
            {
                timer.Reload();
            }
        }

        public void Stop(bool waitForJobsToFinish = true)
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            var didStop = @lock.LockDelegate<bool>(() =>
            {
                if (!isRunning)
                    return false;

                isRunning = false;
                return true;
            });

            if (didStop)
            {
                timer.Pause();
            }
        }

        public async Task StopAsync(bool waitForJobsToFinish = true)
        { 
        
        }

        #region Schedule


        public long ScheduleJob(IJob job, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null)
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            var jobInfo = new JobInfo(
                job: job,
                trigger: trigger,
                key: key);

            var id = jobRegistry.Add(jobInfo);
            timer.Reload();

            return id;
        }
        
        public long ScheduleJob(Type jobType, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null)
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            var jobInfo = new JobInfo(
                jobType: jobType,
                trigger: trigger,
                key: key);

            var id = jobRegistry.Add(jobInfo);
            timer.Reload();

            return id;
        }

        public void RemoveJob(long id)
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();
            
            jobRegistry.Remove(id);
        }

        public void RemoveAllJobs()
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            jobRegistry.RemoveAll();
        }

        #endregion

        public void Dispose()
        {
            if (!ots.TrySet())
                return;

            timer.Dispose();
        }

        #region Internal

        private void OnTimerTriggered()
        {
            if (ots.IsSet)
                return;

#if DEBUG
            Console.WriteLine($"[Scheduler] Timer triggered.");
#endif
            var jobInfos = jobRegistry.BorrowNextJobs();

            foreach(var jobInfo in jobInfos)
            {
#if DEBUG
                Console.WriteLine($"[Scheduler] Executing job [Id={jobInfo.Id},Key={jobInfo.Key}].");
#endif
                Task.Run(() => HandleJobExecution(jobInfo));
            }
        }

        async Task HandleJobExecution(JobInfo jobInfo)
        {
            try
            {
                var jobExecutionContext = new JobExecutionContext(jobInfo, default);

                try
                {
                    //jobExecution.Started = DateTimeOffset.UtcNow;

                    var job = jobInfo.Job ?? jobFactory.CreateJob(jobInfo.JobType);

                    await job.ExecuteAsync(jobExecutionContext);

                    //jobExecution.Stopped = DateTimeOffset.UtcNow;
                }
                catch (Exception e)
                {
                    //jobExecution.Stopped = DateTimeOffset.UtcNow;
                    //jobExecution.ThrownException = e;
                }

                if (ots.IsSet)
                    return;

                //jobInfo.AddNewExecution(jobExecution);
                jobRegistry.ReturnBorrowedJob(jobInfo.Id);
                timer.Reload();
            }
            catch
            {
                // ignore
            }
        }

        #endregion
    }
}
