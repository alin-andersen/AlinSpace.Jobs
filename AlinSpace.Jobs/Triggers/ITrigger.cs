namespace AlinSpace.Jobs
{
    public interface ITrigger
    {
        TimeSpan GetDueTime(IJobInfo jobInfo);
    }
}
