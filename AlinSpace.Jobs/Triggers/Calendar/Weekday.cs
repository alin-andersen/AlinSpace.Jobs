namespace AlinSpace.Jobs
{
    public class Weekday
    {
        public int Day { get; }

        public Weekday(int day)
        {
            if (day < 1)
                throw new Exception();

            if (day > 7)
                throw new Exception();

            Day = day;
        }
    }
}
