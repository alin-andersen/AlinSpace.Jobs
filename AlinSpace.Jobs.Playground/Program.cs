namespace AlinSpace.Jobs.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.ScheduleJob<JobA>(Trigger.Recurring(TimeSpan.FromSeconds(1), times: 10));
            scheduler.ScheduleJob<JobB>(Trigger.Recurring(TimeSpan.FromSeconds(1), times: 10));

            Thread.Sleep(100000);
        }
    }
}