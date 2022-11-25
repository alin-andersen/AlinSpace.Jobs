namespace AlinSpace.Jobs.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.ScheduleJob<MyJob>(Trigger.OneShot());

            Console.ReadLine();
        }
    }
}