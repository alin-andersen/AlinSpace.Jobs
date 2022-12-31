namespace AlinSpace.Jobs
{
    public class CalendarTriggerBuilder
    {
        public IList<CalendarTime> Times { get; } = new List<CalendarTime>();

        public CalendarTrigger Build()
        {
            return new CalendarTrigger(Times);
        }

        public CalendarTriggerBuilder OnDaysOfYear(params int[] daysOfYear)
        {
            foreach (var dayOfYear in daysOfYear)
            {
                Times.Add(new CalendarTime(dayOfYear: new DayOfYear(dayOfYear)));
            }

            return this;
        }

        public CalendarTriggerBuilder OnDaysOfMonth(params int[] daysOfMonth)
        {
            foreach (var dayOfMonth in daysOfMonth)
            {
                Times.Add(new CalendarTime(dayOfMonth: new DayOfMonth(dayOfMonth)));
            }

            return this;
        }

        public CalendarTriggerBuilder OnWeekdays(params int[] weekdays)
        {
            foreach(var weekday in weekdays)
            {
                Times.Add(new CalendarTime(weekday: new Weekday(weekday)));
            }

            return this;
        }

        public CalendarTriggerBuilder OnTimeOfDay(int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
        {
            Times.Add(new CalendarTime(timeOfDay: new TimeOfDay(hour, minute, second, millisecond)));
            return this;
        }
    }
}
