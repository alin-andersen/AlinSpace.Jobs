namespace AlinSpace.Jobs
{
    public class DefaultJobFactory : IJobFactory
    {
        public T CreateJob<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
