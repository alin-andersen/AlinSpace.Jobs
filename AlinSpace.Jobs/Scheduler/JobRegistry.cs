using System.Collections.Immutable;

namespace AlinSpace.Jobs
{
    internal class JobRegistry
    {
        private ImmutableDictionary<long, JobInfo> jobs = ImmutableDictionary<long, JobInfo>.Empty;
        private ImmutableDictionary<object, JobInfo> jobsWithKey = ImmutableDictionary<object, JobInfo>.Empty;
        private SpinLock jobsSpinLock = new();

        public IEnumerable<IJobInfo> Jobs => jobs.Values;

        public long Add(JobInfo jobInfo)
        {
            jobInfo.Id = JobId.New();

            jobsSpinLock.LockDelegate(() =>
            {
                jobs = jobs.Add(jobInfo.Id, jobInfo);
                jobsWithKey = jobsWithKey.Add(jobInfo.Key ?? "null", jobInfo);
            });

            return jobInfo.Id;
        }

        public void UpdateJobInfo(long id, Action<JobInfo> update)
        {
            jobsSpinLock.LockDelegate(() =>
            {
                if (jobs.TryGetValue(id, out var jobInfo))
                {
                    update(jobInfo);
                }
            });
        }

        public TimeSpan? GetGetDueTime()
        {
            return jobs.Values.Select(x => x.Trigger.GetDueTime(x)).OrderBy(x => x).FirstOrDefault();
        }

        IEnumerable<JobInfo> GetPendingJobInfos()
        {
            var jobInfos = new List<(JobInfo, TimeSpan)>();

            foreach (var jobInfo in jobs.Values.Where(x => x.State == JobState.Waiting))
            {
                var dueTime = jobInfo.Trigger.GetDueTime(jobInfo);

                if (dueTime > TimeSpan.Zero)
                    continue;

                jobInfos.Add((jobInfo, dueTime));
            }

            return jobInfos
                .OrderBy(x => x.Item2)
                .Select(x => x.Item1);
        }

        public IEnumerable<JobInfo> TakeNextPendingJobs()
        {
            var pendingJobInfos = GetPendingJobInfos();

            var acceptedJobInfos = new List<JobInfo>();
            
            foreach(var pendingJobInfo in pendingJobInfos)
            {
                var acceptedJobInfo = jobsSpinLock.LockDelegate<JobInfo>(() =>
                {
                    if (pendingJobInfo.State != JobState.Waiting)
                        return null;

                    pendingJobInfo.State = JobState.Running;
                    return pendingJobInfo;
                });

                acceptedJobInfos.Add(acceptedJobInfo);
            }

            return acceptedJobInfos;
        }

        public void JobExecuted(long id)
        {
            jobsSpinLock.LockDelegate(() =>
            {
                if (jobs.TryGetValue(id, out var jobInfo))
                {
                    jobInfo.State = JobState.Waiting;
                }
            });
        }
    }
}
