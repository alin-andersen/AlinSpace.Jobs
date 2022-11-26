namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the quota.
    /// </summary>
    public sealed class Quota
    {
        /// <summary>
        /// Gets the duration of the quota.
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// Gets the number of times per duration.
        /// </summary>
        public int Times { get; }

        /// <summary>
        /// Gets the interval of the quota.
        /// </summary>
        public TimeSpan Interval => Duration.Divide(Times);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="duration">Duration.</param>
        /// <param name="times">Times per duration.</param>
        public Quota(TimeSpan duration, int times)
        {
            Duration = duration;
            Times = times;
        }

        /// <summary>
        /// Creates a day quota.
        /// </summary>
        /// <param name="times">Times per day.</param>
        /// <returns>Quota.</returns>
        public static Quota Day(int times)
        {
            return new Quota(
                TimeSpan.FromDays(1),
                times);
        }

        /// <summary>
        /// Creates a days quota.
        /// </summary>
        /// <param name="days">Duration in days.</param>
        /// <param name="times">Times per days.</param>
        /// <returns>Quota.</returns>
        public static Quota Days(int days, int times)
        {
            return new Quota(
                TimeSpan.FromDays(days),
                times);
        }

        /// <summary>
        /// Creates a month quota.
        /// </summary>
        /// <param name="times">Times per month.</param>
        /// <returns>Quota.</returns>
        public static Quota Month(int times)
        {
            return new Quota(
                TimeSpan.FromDays(31),
                times);
        }

        /// <summary>
        /// Creates a months quota.
        /// </summary>
        /// <param name="months">Duration in months.</param>
        /// <param name="times">Times per months.</param>
        /// <returns>Quota.</returns>
        public static Quota Months(int months, int times)
        {
            return new Quota(
                TimeSpan.FromDays(31 * months),
                times);
        }

        /// <summary>
        /// Creates a year quota.
        /// </summary>
        /// <param name="times">Times per year.</param>
        /// <returns>Quota.</returns>
        public static Quota Year(int times)
        {
            return new Quota(
                TimeSpan.FromDays(365),
                times);
        }

        /// <summary>
        /// Creates a years quota.
        /// </summary>
        /// <param name="years">Duration in years.</param>
        /// <param name="times">Times per years.</param>
        /// <returns>Quota.</returns>
        public static Quota Years(int years, int times)
        {
            return new Quota(
                TimeSpan.FromDays(365 * years),
                times);
        }
    }
}
