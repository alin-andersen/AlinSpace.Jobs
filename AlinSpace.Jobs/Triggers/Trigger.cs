using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlinSpace.Jobs
{
    public static class Trigger
    {
        public static ITrigger OneShot(TimeSpan? delay = null)
        {
            return new OneShotTrigger(delay);
        }

        public static ITrigger Recurring(Action<IRecurringTriggerConfiguration> configure)
        {

        }
    }
}
