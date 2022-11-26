namespace AlinSpace.Jobs
{
    /// <summary>
    /// Represents the job state.
    /// </summary>
    public enum JobState
    {
        /// <summary>
        /// Job is waiting for execution.
        /// </summary>
        Waiting = 0,

        /// <summary>
        /// Job is paused.
        /// </summary>
        Paused = 1,

        /// <summary>
        /// Job is running.
        /// </summary>
        Running = 2,
    }
}
