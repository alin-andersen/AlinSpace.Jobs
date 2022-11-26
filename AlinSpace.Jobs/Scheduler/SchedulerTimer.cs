namespace AlinSpace.Jobs
{
    internal class SchedulerTimer : IDisposable
    {
        private readonly JobRegistry jobRegistry;
        private readonly Timer timer;

        private OneTimeSwitch ots = new();
        private SpinLock @lock = new();

        public SchedulerTimer(
            JobRegistry jobRegistry,
            Action onTrigger)
        {
            this.jobRegistry = jobRegistry;
            timer = new Timer(_ => onTrigger());
        }

        void SetToFireIn(TimeSpan dueTime)
        {
            if (dueTime < TimeSpan.Zero)
                dueTime = TimeSpan.Zero;
#if DEBUG
            Console.WriteLine($"[Scheduler] Timer set to fire in {dueTime.TotalSeconds} seconds.");
#endif
            @lock.LockDelegate(() =>
            {
                timer.Change(dueTime, Timeout.InfiniteTimeSpan);
            });
        }

        public void Reload()
        {
            ots.ThrowObjectDisposedIfSet<SchedulerTimer>();

            var dueTime = jobRegistry.GetGetDueTime();

            if (dueTime.HasValue)
            {
                SetToFireIn(dueTime.Value);
            }
            else
            {
                Pause();
            }
        }

        public void Pause()
        {
            ots.ThrowObjectDisposedIfSet<SchedulerTimer>();
#if DEBUG
            Console.WriteLine($"[Scheduler] Timer paused.");
#endif
            @lock.LockDelegate(() =>
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            });
        }

        public void Dispose()
        {
            if (!ots.TrySet())
                return;

            timer.Dispose();
        }
    }
}
