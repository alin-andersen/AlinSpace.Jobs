namespace AlinSpace.Jobs.Playground
{
    public class JobD : IJob
    {
        public static int Counter { get; set; }

        public async Task ExecuteAsync(IJobExecutionContext context)
        {
            //Console.WriteLine($"[JobId={context.Info.Id},Execution={context.Info.NumberOfExecutions + 1}] JOB D {DateTime.UtcNow.ToLongTimeString()} ...");
            await Task.Delay(TimeSpan.FromMilliseconds(1));
            Counter += 1;
        }
    }
}