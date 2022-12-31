namespace AlinSpace.Jobs
{
    public class DayOfYear
    {
        public int Day { get; }

        public DayOfYear(int day)
        {
            if (day < 1)
                throw new Exception();

            if (day > 365)
                throw new Exception();

            Day = day;
        }
    }
}