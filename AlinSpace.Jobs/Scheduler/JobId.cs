namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job ID.
    /// </summary>
    internal static class JobId
    {
        private static long id = 0;

        public static long New()
        {
            return Interlocked.Increment(ref id);
        }
    }
}
