namespace HotelSmartManagement.EmployeeSelfService.MVVM.Models
{
    public enum EmployeeStatus
    {
        FullTime,
        PartTime,
        Casual,
        Inactive
    }

    public static class EmployeeStatusExtensions
    {
        public static string ToFriendlyString(this EmployeeStatus status, int hoursWorkedPerWeek = 0)
        {
            return status switch
            {
                EmployeeStatus.FullTime => "Full-time (38hr p/week)",
                EmployeeStatus.PartTime => $"Part-time ({hoursWorkedPerWeek}hr p/week)",
                EmployeeStatus.Casual => $"Casual (Guaranteed Hours p/week: {hoursWorkedPerWeek})",
                EmployeeStatus.Inactive => "Inactive",
                _ => "Unknown status"
            };
        }
    }
}
