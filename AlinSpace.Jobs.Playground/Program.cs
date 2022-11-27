namespace AlinSpace.Jobs.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.ScheduleJob<MyJob>(Trigger.Recurring(TimeSpan.FromSeconds(5), times: 5));
            
            Thread.Sleep(2000);
        }
    }
}