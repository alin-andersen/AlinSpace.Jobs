using System.Runtime;
using System.Runtime.CompilerServices;

namespace AlinSpace.Jobs
{
    public class Scheduler : IScheduler
    {
        private readonly IJobExecutor jobExecutor;
        private readonly IJobFactory jobFactory;

        public Scheduler(IJobExecutor jobExecutor, IJobFactory? jobFactory = null)
        {
            this.jobExecutor = jobExecutor ?? new DefaultJobExecutor();
            this.jobFactory = jobFactory ?? new DefaultJobFactory();

            timer = new SchedulerTimer(OnTimerTriggered);
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            lock(jobExecutor)
            {
                jobExecutor.Start();
                IsRunning = true;
            }
        }

        public void Stop(bool waitForJobsToFinish = true)
        {
            lock (jobExecutor)
            {
                jobExecutor.Stop(waitForJobsToFinish);
                IsRunning = false;
            }
        }

        #region Schedule

        public IEnumerable<IJobInfo> Jobs => jobRegistry.Jobs;

        public long ScheduleJob(IJob job, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null)
        {
            var jobInfo = new JobInfo(
                job: job,
                trigger: trigger,
                key: key);

            var id = jobRegistry.Add(jobInfo);
            UpdateTimer();

            return id;
        }
        
        public long ScheduleJob(Type jobType, ITrigger trigger, IEnumerable<object>? parameters = null, object? key = null)
        {
            var jobInfo = new JobInfo(
                jobType: jobType,
                trigger: trigger,
                key: key);

            var id = jobRegistry.Add(jobInfo);
            UpdateTimer();

            return id;
        }

        public void RemoveJob(long id)
        {

        }

        public void RemoveAllJobs()
        {

        }

        public void UnpauseJob(long id)
        {

        }


        public void PauseJob(long id)
        {

        }

        #endregion

        public void Dispose()
        {
            timer?.Dispose();
            timer = null;


        }

        #region Internal

        private SchedulerTimer timer;
        private readonly JobRegistry jobRegistry = new();

        void UpdateTimer()
        {
            var dueTime = jobRegistry.GetGetDueTime();
            timer.Change(dueTime ?? Timeout.InfiniteTimeSpan);
        }

        private void OnTimerTriggered()
        {
            var jobInfos = jobRegistry.TakeNextPendingJobs();

            foreach(var jobInfo in jobInfos)
            {

                Task.Run(() => SupportJobExecution(jobInfo));

                
            }
        }

        async Task SupportJobExecution(JobInfo jobInfo)
        {
            var job = jobInfo.Job ?? jobFactory.CreateJob(jobInfo.JobType);

            var jobExecution = new JobExection();
            var jobExecutionContext = new JobExecutionContext(jobInfo, null, default);

            try
            {
                jobExecution.StartTimestamp = DateTimeOffset.UtcNow;
                
                await jobExecutor.RunAsync(job, jobExecutionContext);
                
                jobExecution.StartTimestamp = DateTimeOffset.UtcNow;

            }
            catch (Exception e)
            {
                jobExecution.StartTimestamp = DateTimeOffset.UtcNow;
                jobExecution.ThrownException = e;

            }
        }

        #endregion
    }
}
