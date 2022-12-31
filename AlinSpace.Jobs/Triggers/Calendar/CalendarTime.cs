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
    }
}
