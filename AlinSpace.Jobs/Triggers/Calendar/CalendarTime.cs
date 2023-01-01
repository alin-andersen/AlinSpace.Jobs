namespace AlinSpace.Jobs
{
    public class CalendarTime
    {
        public DayOfYear? DayOfYear { get; }

        public Weekday? Weekday { get; }
        
        public DayOfMonth? DayOfMonth { get; }

        public TimeOfDay? TimeOfDay { get; }

        public bool Utc { get; }

        public CalendarTime(
            DayOfYear? dayOfYear = null,
            Weekday? weekday = null,
            DayOfMonth? dayOfMonth = null, 
            TimeOfDay? timeOfDay = null,
            bool utc = true)
        {
            DayOfYear = dayOfYear;
            Weekday = weekday;
            DayOfMonth = dayOfMonth;
            TimeOfDay = timeOfDay;
            Utc = utc;
        }

        public IList<DateTimeOffset> Calculate()
        {
            var timestamps = new List<DateTimeOffset>();

            var dayOfYear = DayOfYear ?? DayOfYear.All;

            for(int day = 1; day <= 365; day++)
            {
                //if (dayOfYear.Day.HasValue)
                //{
                //    if (dayOfYear.Day != day)
                //        continue;
                //}

                var timeOfDay = TimeOfDay ?? TimeOfDay.Zero;

                var d = new DateTimeOffset(
                    0, 0, 0,
                    hour: timeOfDay.Hour,
                    minute: timeOfDay.Minute,
                    second: timeOfDay.Second,
                    offset: TimeSpan.Zero);


            }

            return timestamps;
        }
    }
}
