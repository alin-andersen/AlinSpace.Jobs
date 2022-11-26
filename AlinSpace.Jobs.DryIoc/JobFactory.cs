using DryIoc;

namespace AlinSpace.Jobs.DryIoc
{
    public class JobFactory : IJobFactory
    {
        private readonly IContainer container;

        public JobFactory(IContainer container)
        {
            this.container = container;
        }

        public IJob CreateJob(Type jobType)
        {
            return (IJob)container.Resolve(jobType);
        }
    }
}