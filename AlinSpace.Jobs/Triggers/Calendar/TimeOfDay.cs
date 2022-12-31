namespace AlinSpace.Jobs
{
    public sealed class TimeOfDay
    {
        public int Hour { get; }

        public int Minute { get; }

        public int Second { get; }

        public int Millisecond { get; }

        public TimeOfDay(
            int hour, 
            int minute, 
            int second, 
            int millisecond)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;
        }

        public static TimeOfDay New(
            int hour,
            int minute,
            int second,
            int millisecond)
        {
            return new TimeOfDay(
                hour,
                minute,
                second,
                millisecond);
        }

        public static TimeOfDay Zero()
        {
            return new TimeOfDay(0, 0, 0, 0);
        }

        public static TimeOfDay Midday()
        {
            return new TimeOfDay(12, 0, 0, 0);
        }
    }
}
