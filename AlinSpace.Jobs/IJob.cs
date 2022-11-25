namespace AlinSpace.Jobs
{
    public interface IJob
    {
        Task ExecuteAsync(IJobExecutionContext context);
    }
}