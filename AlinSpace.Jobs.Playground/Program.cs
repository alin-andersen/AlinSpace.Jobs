namespace AlinSpace.Jobs.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new Scheduler();

            scheduler.Start();

            //scheduler.ScheduleJob<MyJob>(Trigger.Recurring(TimeSpan.FromSeconds(5), times: 5));
            scheduler.ScheduleJob<MyJob>(Trigger.Recurring(Quota.Day(1), 2, TimeSpan.FromSeconds(5)));
            
            Console.ReadLine();
        }
    }
}