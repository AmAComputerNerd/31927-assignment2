namespace HotelSmartManagement.HotelOverview.MVVM.Models
{
    public enum AnnouncementCategory
    {
        Shortage,
        Maintenance,
        Incident,
        Other
    }

    public static class AnnouncementCategoryExtensions
    {
        public static string ToFriendlyString(this AnnouncementCategory AnnouncementCategory)
        {
            return AnnouncementCategory switch
            {
                AnnouncementCategory.Shortage => "Item Shortage",
                AnnouncementCategory.Maintenance => "Maintenance Task",
                AnnouncementCategory.Incident => "Incident to Resolve",
                AnnouncementCategory.Other => "Other Task",
                _ => "Unknown Task"
            };
        }
    }
}
