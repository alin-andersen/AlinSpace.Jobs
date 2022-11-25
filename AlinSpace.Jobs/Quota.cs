namespace AlinSpace.Jobs
{
    public class Quota
    {
        public TimeSpan Duration { get; }

        public int Times { get; }

        public TimeSpan Interval => Duration.Divide(Times);

        public Quota(TimeSpan duration, int times)
        {
            Duration = duration;
            Times = times;
        }

        public static Quota Day(int times)
        {
            return new Quota(
                TimeSpan.FromDays(1),
                times);
        }

        public static Quota Days(int days, int times)
        {
            return new Quota(
                TimeSpan.FromDays(days),
                times);
        }

        public static Quota Months(int months, int times)
        {
            return new Quota(
                TimeSpan.FromDays(31 * months),
                times);
        }

        public static Quota Year(int times)
        {
            return new Quota(
                TimeSpan.FromDays(365),
                times);
        }

        public static Quota Years(int years, int times)
        {
            return new Quota(
                TimeSpan.FromDays(365 * years),
                times);
        }
    }
}
