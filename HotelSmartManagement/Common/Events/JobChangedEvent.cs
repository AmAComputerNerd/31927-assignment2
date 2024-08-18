namespace HotelSmartManagement.Common.Events
{
    public class JobChangedEvent
    {
        public Guid JobId { get; set; }

        public JobChangedEvent(Guid jobId)
        {
            JobId = jobId;
        }
    }
}
