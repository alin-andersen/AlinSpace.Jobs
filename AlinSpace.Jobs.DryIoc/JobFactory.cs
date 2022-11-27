using DryIoc;

namespace AlinSpace.Jobs.DryIoc
{
    /// <summary>
    /// Represents the job factory for DryIoc.
    /// </summary>
    public class JobFactory : IJobFactory
    {
        private readonly IContainer container;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="container">Container.</param>
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