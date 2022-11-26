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

                if (jobInfo.Key != null)
                {
                    jobsWithKey = jobsWithKey.Add(jobInfo.Key, jobInfo);
                }
            });

            return jobInfo.Id;
        }


        public void Remove(long id)
        {
            jobsSpinLock.LockDelegate(() =>
            {
                if (jobs.TryGetValue(id, out var jobInfo))
                {
                    if (jobInfo.State != JobState.Running)
                    {
                        jobs = jobs.Remove(id);
                    }
                    else
                    {
                        jobInfo.IsRemoved = true;
                    }
                }
            });
        }

        public TimeSpan? GetGetDueTime()
        {
            return jobs.Values.Select(x => x.Trigger.GetDueTime(x)).OrderBy(x => x).FirstOrDefault();
        }

        IEnumerable<JobInfo> GetNextJobInfos()
        {
            var jobInfos = new List<(JobInfo, TimeSpan)>();

            foreach (var jobInfo in jobs.Values.Where(x => x.State == JobState.Waiting))
            {
                var dueTime = jobInfo.Trigger.GetDueTime(jobInfo);

                if (!dueTime.HasValue || dueTime > TimeSpan.Zero)
                    continue;

                jobInfos.Add((jobInfo, dueTime.Value));
            }

            return jobInfos
                .OrderBy(x => x.Item2)
                .Select(x => x.Item1);
        }

        public IEnumerable<JobInfo> BorrowNextJobs()
        {
            var nextJobs = GetNextJobInfos();

            var borrowedJobs = new List<JobInfo>();
            
            foreach(var nextJob in nextJobs)
            {
                var borrowedJob = jobsSpinLock.LockDelegate<JobInfo>(() =>
                {
                    if (nextJob.State != JobState.Waiting)
                        return null;

                    nextJob.State = JobState.Running;
                    return nextJob;
                });

                borrowedJobs.Add(borrowedJob);
            }

            return borrowedJobs;
        }

        public void ReturnBorrowedJob(long id)
        {
            jobsSpinLock.LockDelegate(() =>
            {
                if (jobs.TryGetValue(id, out var jobInfo))
                {
                    if (jobInfo.IsRemoved)
                    {
                        jobs = jobs.Remove(id);
                    }
                    else
                    {
                        jobInfo.State = JobState.Waiting;
                    }
                }
            });
        }

    }
}
