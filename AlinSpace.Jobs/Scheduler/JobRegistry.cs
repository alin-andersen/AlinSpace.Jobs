using System.Collections.Immutable;

namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job registry.
    /// </summary>
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

        public void RemoveAll()
        {
            jobsSpinLock.LockDelegate(() =>
            {
                foreach(var job in jobs.Values)
                {
                    if (job.State != JobState.Running)
                    {
                        jobs = jobs.Remove(job.Id);
                    }
                    else
                    {
                        job.IsRemoved = true;
                    }
                }
            });
        }

        public TimeSpan? GetDueTime()
        {
            return jobs
                .Values
                .Where(x => x.State == JobState.Waiting)
                .Select(x => x.Trigger.GetDueTime(x))
                .Where(x => x != null)
                .OrderBy(x => x).FirstOrDefault();
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

        public IEnumerable<JobInfo> LockNextJobs()
        {
            var nextJobs = GetNextJobInfos();

            var lockedJobs = new List<JobInfo>();
            
            foreach(var nextJob in nextJobs)
            {
                var lockedJob = jobsSpinLock.LockDelegate<JobInfo>(() =>
                {
#pragma warning disable CS8603 // Possible null reference return.
                    if (nextJob.State != JobState.Waiting)
                        return null;
#pragma warning restore CS8603 // Possible null reference return.

                    nextJob.State = JobState.Running;
                    return nextJob;
                });

                if (lockedJob == null)
                    continue;

                lockedJobs.Add(lockedJob);
            }

            return lockedJobs;
        }

        public void UnlockJob(long id)
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
