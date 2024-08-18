namespace HotelSmartManagement.Common.Events
{
    public class LeaveRequestChangedEvent
    {
        public Guid LeaveRequestId { get; set; }

        public LeaveRequestChangedEvent(Guid leaveRequestId)
        {
            LeaveRequestId = leaveRequestId;
        }
    }
}
