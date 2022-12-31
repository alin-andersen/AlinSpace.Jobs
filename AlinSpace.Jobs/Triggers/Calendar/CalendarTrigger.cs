namespace AlinSpace.Jobs
{
    public class CalendarTrigger : ITrigger
    {
        private readonly IList<CalendarTime> times;

        public CalendarTrigger(IEnumerable<CalendarTime> times)
        {
            this.times = times?.ToList() ?? new List<CalendarTime>();
        }

        public TimeSpan? GetDueTime(IJobInfo jobInfo)
        {
            var currentUtcTime = DateTime.UtcNow;
            var currentLocalTime = DateTime.Now;



            foreach(var time in times)
            {

            }


            return null;
        }
    }
}
