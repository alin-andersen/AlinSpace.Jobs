namespace AlinSpace.Jobs
{
    public static class JobExecutionId
    {
        private static long id = 0;

        public static long New()
        {
            return Interlocked.Increment(ref id);
        }
    }
}
