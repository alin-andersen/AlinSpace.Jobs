namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the scheduler.
    /// </summary>
    public class Scheduler : IScheduler
    {
        private readonly IJobFactory jobFactory;

        private readonly OneTimeSwitch ots = new();

        private readonly JobRegistry jobRegistry;
        private readonly JobExecutionTracker jobExecutionsTracker;
        private readonly SchedulerTimer timer;

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
            jobExecutionsTracker = new JobExecutionTracker();
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
                timer.Start();
            }
        }

        public void Stop(bool waitForJobsToFinish = true)
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            var didStop = @lock.LockDelegate(() =>
            {
                if (!isRunning)
                    return false;

                isRunning = false;
                return true;
            });

            if (didStop)
            {
                timer.Stop();
            }

            if (waitForJobsToFinish)
            {
#if DEBUG
                Console.WriteLine($"[Scheduler] Waiting for job executions to finish ...");
#endif
                jobExecutionsTracker.WaitForJobsToFinishExecution();
#if DEBUG
                Console.WriteLine($"[Scheduler] Job executions finished.");
#endif
            }
        }

        #region Schedule

        public long ScheduleJob(IJob job, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null)
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            var jobInfo = new JobInfo(
                job: job,
                trigger: trigger,
                key: key);

            var id = @lock.LockDelegate(() =>
            {
                var id = jobRegistry.Add(jobInfo);
                timer.Reload();
                return id;
            });

            return id;
        }
        
        public long ScheduleJob(Type jobType, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null)
        {
            ots.ThrowObjectDisposedIfSet<Scheduler>();

            var jobInfo = new JobInfo(
                jobType: jobType,
                trigger: trigger,
                key: key);

            var id = @lock.LockDelegate(() =>
            {
                var id = jobRegistry.Add(jobInfo);
                timer.Reload();
                return id;
            });

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

            try
            {
                Stop();
            }
            catch
            {
                // ignore
            }

            try
            {
                jobExecutionsTracker.Dispose();
            }
            catch
            {
                // ignore
            }

            try
            {
                timer.Dispose();
            }
            catch
            {
                // ignore
            }

            try
            {
                jobRegistry.RemoveAll();
            }
            catch
            {
                // ignore
            }
        }

        #region Internal

        private void OnTimerTriggered()
        {
            if (ots.IsSet)
                return;

            if (!timer.IsRunning)
                return;
#if DEBUG
            Console.WriteLine($"[Scheduler] Timer triggered.");
#endif
            var jobInfos = jobRegistry.LockNextJobs();
#if DEBUG
            Console.WriteLine($"[Scheduler] Jobs waiting for execution: {jobInfos.Count()}");
#endif
            if (jobInfos.Empty())
            {
                timer.Reload();
                return;
            }

            foreach (var jobInfo in jobInfos)
            {
                Task.Run(() => HandleJobExecution(jobInfo));
            }
        }

        async Task HandleJobExecution(JobInfo jobInfo)
        {
            jobExecutionsTracker.JobExecutionStarted();
#if DEBUG
            Console.WriteLine($"[Scheduler] Executing job [Id={jobInfo.Id}] ...");
#endif
            var jobExecutionContext = new JobExecutionContext(jobInfo, default);

            try
            {
                //jobExecution.Started = DateTimeOffset.UtcNow;

#pragma warning disable CS8604 // Possible null reference argument.
                var job = jobInfo.Job ?? jobFactory.CreateJob(jobInfo.JobType);
#pragma warning restore CS8604 // Possible null reference argument.


                await job.ExecuteAsync(jobExecutionContext);

                //jobExecution.Stopped = DateTimeOffset.UtcNow;
            }
            catch (Exception e)
            {
                //jobExecution.Stopped = DateTimeOffset.UtcNow;
                //jobExecution.ThrownException = e;
            }
            finally
            {
                jobInfo.NumberOfExecutions += 1;

                //jobInfo.AddNewExecution(jobExecution);
                jobRegistry.UnlockJob(jobInfo.Id);

                jobExecutionsTracker.JobExecutionStopped();
            }

            if (ots.IsSet)
                return;

            @lock.LockDelegate(() => timer.Reload());
        }

        #endregion
    }
}
