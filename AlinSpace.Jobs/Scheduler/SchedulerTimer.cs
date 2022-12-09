namespace AlinSpace.Jobs
{
    internal class SchedulerTimer : IDisposable
    {
        private readonly JobRegistry jobRegistry;
        private readonly Timer timer;

        private OneTimeSwitch ots = new();
        private SpinLock @lock = new();

        private bool running = false;

        public SchedulerTimer(
            JobRegistry jobRegistry,
            Action onTrigger)
        {
            this.jobRegistry = jobRegistry;
            timer = new Timer(_ => onTrigger());
        }

        public bool IsRunning => running;

        public void Start()
        {
            ots.ThrowObjectDisposedIfSet<SchedulerTimer>();
#if DEBUG
            Console.WriteLine($"[Scheduler Timer] Starting timer ...");
#endif
            @lock.LockDelegate(() =>
            {
                running = true;
            });

            Reload();
        }

        public void Stop()
        {
            ots.ThrowObjectDisposedIfSet<SchedulerTimer>();
#if DEBUG
            Console.WriteLine($"[Scheduler Timer] Stopping timer ...");
#endif
            @lock.LockDelegate(() =>
            {
                running = false;
            });
        }

        public void Reload()
        {
            ots.ThrowObjectDisposedIfSet<SchedulerTimer>();

            if (!IsRunning)
                return;

            var dueTime = jobRegistry.GetDueTime();

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

            if (!IsRunning)
                return;

            @lock.LockDelegate(() =>
            {
                if (!running) return;
#if DEBUG
                Console.WriteLine($"[Scheduler Timer] Timer paused.");
#endif
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            });
        }

        void SetToFireIn(TimeSpan dueTime)
        {
            if (dueTime < TimeSpan.Zero)
                dueTime = TimeSpan.Zero;

            @lock.LockDelegate(() =>
            {
                if (!running) return;
#if DEBUG
                Console.WriteLine($"[Scheduler Timer] Timer set to fire in {dueTime.TotalSeconds} seconds.");
#endif
                timer.Change(dueTime, Timeout.InfiniteTimeSpan);
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
