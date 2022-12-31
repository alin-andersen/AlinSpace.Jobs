namespace AlinSpace.Jobs
{
    public class DayOfMonth
    {
        public int Day { get; }

        public DayOfMonth(int day)
        {
            if (day < 1)
                throw new Exception();

            if (day > 31)
                throw new Exception();

            Day = day;
        }
    }
}