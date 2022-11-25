namespace AlinSpace.Jobs
{
    public interface IJobFactory
    {
        IJob CreateJob(Type jobType);
    }
}
