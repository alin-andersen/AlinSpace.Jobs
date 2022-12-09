namespace AlinSpace.Jobs.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.ScheduleJob<JobA>(Trigger.Recurring(TimeSpan.FromMilliseconds(500), times: 3));
            scheduler.ScheduleJob<JobB>(Trigger.Recurring(TimeSpan.FromMilliseconds(500), times: 3));

            //while(true)
            //{
            //    Thread.Sleep(TimeSpan.FromSeconds(1));

            //    if (JobA.Counter == 3 &&
            //        JobB.Counter == 3)
            //        break;
            //}

            Thread.Sleep(1000);

            scheduler.Stop(true);
        }
    }
}