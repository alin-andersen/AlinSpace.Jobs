namespace AlinSpace.Jobs.UnitTests
{
    public class SchedulerTests
    {
        [Fact]
        public void Create_Scheduler_Start_Job_Then_Dispose()
        {
            using var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.ScheduleJob<MyJob>(Trigger.OneShot());

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Create_Scheduler_Start_Job_Then_Stop_Then_Dispose()
        {
            using var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.ScheduleJob<MyJob>(Trigger.OneShot());

            Thread.Sleep(TimeSpan.FromSeconds(1));

            scheduler.Stop();
        }
    }
}