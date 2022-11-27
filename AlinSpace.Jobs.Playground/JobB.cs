namespace AlinSpace.Jobs.Playground
{
    public class JobB : IJob
    {
        public async Task ExecuteAsync(IJobExecutionContext context)
        {
            Console.WriteLine($"[JobId={context.Info.Id},Execution={context.Info.NumberOfExecutions + 1}] JOB B {DateTime.UtcNow.ToLongTimeString()} ...");
            await Task.Delay(100);
        }
    }
}