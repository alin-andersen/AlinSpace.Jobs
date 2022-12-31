namespace AlinSpace.Jobs.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.ScheduleJob<JobA>(Trigger.Recurring(TimeSpan.FromMilliseconds(1), times: 1000));
            scheduler.ScheduleJob<JobB>(Trigger.Recurring(TimeSpan.FromMilliseconds(2), times: 1000));
            scheduler.ScheduleJob<JobC>(Trigger.Recurring(TimeSpan.FromMilliseconds(2), times: 1000));
            scheduler.ScheduleJob<JobD>(Trigger.Recurring(TimeSpan.FromMilliseconds(1), times: 1000));

            //while(true)
            //{
            //    Thread.Sleep(TimeSpan.FromSeconds(1));

            //    if (JobA.Counter == 3 &&
            //        JobB.Counter == 3)
            //        break;
            //}

            Console.ReadLine();

            scheduler.Stop(true);
        }
    }
}