namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the default job factory.
    /// </summary>
    public class DefaultJobFactory : IJobFactory
    {
        public IJob CreateJob(Type jobType)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
            return (IJob)Activator.CreateInstance(jobType);
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }
    }
}
