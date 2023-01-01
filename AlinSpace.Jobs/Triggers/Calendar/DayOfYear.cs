namespace AlinSpace.Jobs
{
    public class DayOfYear
    {
        public IEnumerable<int> Days { get; }

        //public DayOfYear(IEnumerable<int>? days = null)
        //{
        //    if (days != null;l)
        //    {


        //        if (day < 1)
        //            throw new Exception();

        //        if (day > 365)
        //            throw new Exception();
        //    }

        //    Day = day;
        //}

        public static DayOfYear All { get; } = new DayOfYear();
    }
}