namespace HotelSmartManagement.EmployeeSelfService.MVVM.Models
{
    public enum JobUrgencyLevel
    {
        Trivial = 1,
        Low = 2,
        Medium = 3,
        High = 4,
        Critical = 5
    }

    public static class JobUrgencyLevelExtensions
    {
        public static string ToFriendlyString(this JobUrgencyLevel level)
        {
            return level switch
            {
                JobUrgencyLevel.Trivial => "[1] Trivial",
                JobUrgencyLevel.Low => "[2] Low",
                JobUrgencyLevel.Medium => "[3] Medium",
                JobUrgencyLevel.High => "[4] High",
                JobUrgencyLevel.Critical => "[5] Critical",
                _ => "Unknown",
            };
        }

        public static string GetDescription(this JobUrgencyLevel level)
        {
            return level switch
            {
                JobUrgencyLevel.Trivial => "Minor importance; can be addressed if time permits.",
                JobUrgencyLevel.Low => "Mild importance; can be handled in due course.",
                JobUrgencyLevel.Medium => "Moderatate importance; needs to be addressed soon.",
                JobUrgencyLevel.High => "High importance; requires prompt attention.",
                JobUrgencyLevel.Critical => "Critical importance; requires immediate action.",
                _ => "Unknown importance level.",
            };
        }
    }
}
