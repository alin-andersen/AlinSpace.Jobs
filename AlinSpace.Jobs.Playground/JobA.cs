namespace AlinSpace.Jobs.Playground
{
    public class JobA : IJob
    {
        public async Task ExecuteAsync(IJobExecutionContext context)
        {
            Console.WriteLine($"[JobId={context.Info.Id},Execution={context.Info.NumberOfExecutions + 1}] JOB A {DateTime.UtcNow.ToLongTimeString()} ...");
            await Task.Delay(100);
        }
    }
}