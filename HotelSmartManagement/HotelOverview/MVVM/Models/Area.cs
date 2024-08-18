namespace HotelSmartManagement.HotelOverview.MVVM.Models
{
    public enum Area
    {
        Whole,
        Floors,
        Floor,
        Rooms,
        Room,
        Other
    }

    public static class AreaExtensions
    {
        public static string ToFriendlyString(this Area Area)
        {
            return Area switch
            {
                Area.Whole => "Whole Hotel",
                Area.Floors => "Multiple Floors",
                Area.Floor => "Single Floor",
                Area.Rooms => "Multiple Rooms",
                Area.Room => "Single Room",
                Area.Other => "Other Area",
                _ => "Unknown"
            };
        }
    }
}
