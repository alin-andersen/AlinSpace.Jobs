using AlinSpace.Jobs.Triggers;

namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the trigger.
    /// </summary>
    public static class Trigger
    {
        /// <summary>
        /// Creates the one shot trigger.
        /// </summary>
        /// <param name="delay">Delay.</param>
        /// <returns>Trigger.</returns>
        public static ITrigger OneShot(TimeSpan? delay = null)
        {
            return new OneShotTrigger(delay);
        }

        /// <summary>
        /// Creates the one shot trigger.
        /// </summary>
        /// <param name="time">Time.</param>
        /// <returns>Trigger.</returns>
        public static ITrigger OneShot(DateTimeOffset time)
        {
            var delay = time.ToUniversalTime() - DateTimeOffset.UtcNow;
            return new OneShotTrigger(delay);
        }

        /// <summary>
        /// Creates the recurring trigger.
        /// </summary>
        /// <param name="interval">Interval.</param>
        /// <param name="times">Times.</param>
        /// <param name="delay">Delay.</param>
        /// <returns>Trigger.</returns>
        public static ITrigger Recurring(TimeSpan interval, int? times = null, TimeSpan? delay = null)
        {
            return new RecurringTrigger(interval, times, delay);
        }

        /// <summary>
        /// Creates the recurring trigger.
        /// </summary>
        /// <param name="quota">Quota.</param>
        /// <param name="times">Times.</param>
        /// <param name="delay">Delay.</param>
        /// <returns>Trigger.</returns>
        public static ITrigger Recurring(Quota quota, int? times = null, TimeSpan? delay = null)
        {
            return new RecurringTrigger(quota.Interval, times, delay);
        }

        /// <summary>
        /// Creates the calendar trigger.
        /// </summary>
        /// <param name="configure">Configure.</param>
        /// <returns>Calendar trigger.</returns>
        public static ITrigger Calendar(Action<CalendarTriggerBuilder> configure)
        {
            var calendarTriggerBuilder = new CalendarTriggerBuilder();
            configure(calendarTriggerBuilder);

            return calendarTriggerBuilder.Build();
        }
    }
}
