namespace HotelSmartManagement.Common.Events
{
    public class UserChangedEvent
    {
        public bool IsChangeInCurrentUser { get; private set; }

        public UserChangedEvent(bool isLogout)
        {
            IsChangeInCurrentUser = isLogout;
        }
    }
}
