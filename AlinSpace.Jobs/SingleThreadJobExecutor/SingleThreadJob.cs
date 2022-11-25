namespace AlinSpace.Jobs.SingleThreadJobExecutor
{
    internal class SingleThreadJob
    {
        public TaskCompletionSource Tcs { get; } = new TaskCompletionSource();


        public IJobExecutionContext JobExecutionContext { get; set; }
        public IJob Job { get; set; }
    }
}
