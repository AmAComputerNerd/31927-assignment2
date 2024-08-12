using System.Reflection;

namespace HotelSmartManagement.Common.Events
{
    public class UserChangedEvent
    {
        public bool IsLogout { get; private set; }
        public FieldInfo? FieldChanged { get; private set; }

        public UserChangedEvent(bool isLogout, FieldInfo? fieldChanged = default)
        {
            IsLogout = isLogout;
            if (!isLogout)
            {
                FieldChanged = fieldChanged ?? throw new ArgumentNullException($"Sent a UserChangedEvent with IsLogout=false, but didn't provide FieldInfo.");
            }
        }
    }
}
