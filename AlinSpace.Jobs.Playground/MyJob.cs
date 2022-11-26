namespace AlinSpace.Jobs.Playground
{
    public class MyJob : IJob
    {
        public async Task ExecuteAsync(IJobExecutionContext context)
        {
            Console.WriteLine($"[JobId={context.Info.Id},Execution={context.Info.NumberOfExecutions + 1}] Job started execution at {DateTime.UtcNow.ToLongTimeString()} ...");
            
            Console.WriteLine($"[JobId={context.Info.Id},Execution={context.Info.NumberOfExecutions + 1}] Doing some work ...");
            await Task.Delay(2000);
            
            Console.WriteLine($"[JobId={context.Info.Id},Execution={context.Info.NumberOfExecutions + 1}] Job ended execution at {DateTime.UtcNow.ToLongTimeString()} ...");
        }
    }
}