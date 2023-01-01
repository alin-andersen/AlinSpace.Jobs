namespace AlinSpace.Jobs
{
    public class EveryDayTrigger : ITrigger
    {
        private readonly TimeOfDay timeOfDay;


        public TimeSpan? GetDueTime(IJobInfo jobInfo)
        {
            throw new NotImplementedException();
        }
    }
}
