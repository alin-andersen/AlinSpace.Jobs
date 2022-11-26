using DryIoc;

namespace AlinSpace.Jobs.DryIoc
{
    public class JobFactory
    {
        private readonly IContainer container;

        public JobFactory(IContainer container)
        {
            this.container = container;
        }
    }
}