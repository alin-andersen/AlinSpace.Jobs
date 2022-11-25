namespace AlinSpace.Jobs.UnitTests
{
    public class MyJob : IJob
    {
        public int Counter { get; private set; }

        public Task ExecuteAsync(IJobExecutionContext context)
        {
            Counter += 1;
            return Task.CompletedTask;
        }
    }
}