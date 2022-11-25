namespace AlinSpace.Jobs
{
    public class SchedulerTimer : IDisposable
    {
        private readonly Timer timer;

        public SchedulerTimer(Action onTrigger)
        {
            timer = new Timer(_ => onTrigger());
        }

        public void Change(TimeSpan dueTime)
        {
            timer.Change(dueTime, Timeout.InfiniteTimeSpan);
        }

        public void Pause()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
