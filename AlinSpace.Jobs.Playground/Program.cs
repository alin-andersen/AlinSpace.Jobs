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
            //scheduler.ScheduleJob<JobC>(Trigger.Recurring(TimeSpan.FromMilliseconds(2), times: 1000));
            scheduler.ScheduleJob<JobD>(Trigger.Recurring(TimeSpan.FromMilliseconds(1), times: 1000));

            //scheduler.ScheduleJob<JobD>(Trigger.EveryDay(TimeSpan.FromMilliseconds(1), times: 1000));


            //scheduler.ScheduleJob<JobD>(Trigger.Calendar(configure =>
            //{
            //    configure.OnDaysOfMonth(1, 5, 10);
            //}));

            while(true)
            {
                foreach (var jobInfo in scheduler.Jobs)
                {
                    Console.WriteLine($"Job");
                    Console.WriteLine($" - ID={jobInfo.Id}");
                    Console.WriteLine($" - State={jobInfo.State}");
                    Console.WriteLine($" - NumberOfExecutions={jobInfo.NumberOfExecutions}");
                    Console.WriteLine($"");
                }

                Console.ReadLine();
            }
        }
    }
}